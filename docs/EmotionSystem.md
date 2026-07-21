# Emotion System

The emotion system gives players, NPCs, items, and projectiles an `Angry`, `Happy`, `Sad`, or `None` identity. Emotion buffs provide tiered stat changes, the emotion triangle modifies combat damage, and repeated final-tier applications let player effects continue scaling without requiring a separate buff class for every level.

This document describes the implementation in `Content/Systems/EmotionSystem` and the related buff, player, NPC, item, and projectile classes.

Return to the [documentation index](README.md).

## Core concepts

The system uses three related values that should not be confused:

- **Emotion** is the family (`Angry`, `Happy`, `Sad`, or `None`). It is represented by `EmotionType`.
- **Tier** is the progression stage declared by a concrete buff type. The current standard families have tiers 1 through 4.
- **Level** is the value used to scale stats. It normally equals the tier, but a player at the final tier can continue raising the level up to `EmotionStatTuning.PlayerMaxEmotionLevel`.

For example, `Livid` is always the tier-4 Angry buff. Reapplying it can raise a player's effective Angry level above four, but registry queries and emotional-advantage calculations still treat it as tier four.

## Architecture

| Component | Responsibility |
| --- | --- |
| `EmotionTypes.cs` | Defines emotion identities and buff variants. |
| `EmotionStatTuning.cs` | Holds the authoritative duration, limits, advantage multiplier, and stat-scaling values. |
| `EmotionRegistry.cs` | Discovers emotion buffs after content setup, validates tier progressions, and builds lookup tables. |
| `EmotionSystem.cs` | Exposes the gameplay API for queries, application, promotion, removal, and combat resolution. |
| `IEmotionObject` | Common contract for anything associated with an emotion. |
| `IEmotionEntity` | Common runtime-state contract implemented by player and NPC emotion components. |
| `IOnHitEmotionObject` | Marker contract shared by emotion-aware items and projectiles. |
| `EmotionPlayer` | Stores transient player state and retained final-tier scaling state. |
| `EmotionNPC` | Stores per-NPC state, applies immunity defaults, colors emotional NPCs, and connects combat hooks. |
| `EmotionBuff` | Synchronizes buffs into entity state and provides effect, scaling, tooltip, and reapplication hooks. |
| `AngryEmotionBase`, `HappyEmotionBase`, `SadEmotionBase` | Implement the actual stat and combat behavior for each emotion family. |

The intended dependency direction is:

```text
Items / projectiles / tModLoader combat hooks
                    |
                    v
              EmotionSystem
               /         \
              v           v
     EmotionRegistry   IEmotionEntity state
              |           |
              v           v
       EmotionBuff types and family behavior
                    |
                    v
           EmotionStatTuning
```

`EmotionSystem` is the public gameplay entry point. Callers should use it instead of depending on the registry's dictionaries or duplicating buff-list logic.

## Emotion families and effects

The emotion triangle is:

```text
Happy > Angry > Sad > Happy
```

The current family effects are:

| Emotion | Benefit | Cost |
| --- | --- | --- |
| Angry | Increased outgoing damage | Reduced defense |
| Happy | Increased movement speed and critical-hit chance | Increased miss chance |
| Sad | Increased defense and conversion of health damage to mana damage | Reduced movement speed |

Player and NPC values are tuned independently. Family base classes read the appropriate values from `EmotionStatTuning` and apply them through tModLoader hooks.

## Buff tiers and variants

Each concrete emotion buff declares its tier through `EmotionBuff.EmotionTier`.

| Tier | Angry | Happy | Sad |
| ---: | --- | --- | --- |
| 1 | `Angry` | `Happy` | `Sad` |
| 2 | `Enraged` | `Ecstatic` | `Depressed` |
| 3 | `Furious` | `Manic` | `Miserable` |
| 4 | `Livid` | `Hysterical` | `Despondent` |

There are two registration variants:

- `Standard` buffs have normal duration display and participate in tier promotion.
- `NoTime` buffs set `Main.buffNoTimeDisplay[Type]` and are intended for persistent sources such as the emotion flower accessories. They are registered for lookup but cannot be promoted and do not determine a family's maximum tier.

The current no-time buffs are `AngryNoTime`, `HappyNoTime`, and `SadNoTime`, all at tier one.

## Registration and validation

`EmotionRegistry.PostSetupContent` scans every loaded buff after tModLoader finishes registering content. Every `EmotionBuff` supplies three pieces of metadata:

1. Its `Emotion` family.
2. Its declared `EmotionTier`.
3. Its variant, inferred from `Main.buffNoTimeDisplay`.

The registry builds lookups by:

- emotion, tier, and variant;
- inheritance family, tier, and variant;
- buff type to metadata;
- emotion to highest standard tier.

Family registration walks the buff's inheritance chain. As a result, a lookup such as `GetEmotionBuffType<AngryEmotionBase>(2)` can resolve `Enraged` without hard-coding that concrete class in application logic.

Registration fails during setup when:

- a buff declares a tier below one;
- two buffs claim the same emotion/tier/variant key;
- two buffs claim the same family/tier/variant key;
- standard tiers are not contiguous from one through the final tier; or
- a declared final tier exceeds the configured maximum player emotion level.

These failures are intentional. Invalid content should fail early rather than producing ambiguous promotion behavior during gameplay.

## Runtime entity state

Both `EmotionPlayer` and `EmotionNPC` implement `IEmotionEntity`:

- `Emotion` identifies the active family.
- `ActiveEmotionBuff` points to the buff implementing current behavior.
- `EmotionLevel` is the effective stat-scaling level.
- `ImmuneToEmotionChange` controls whether application is allowed.

The state is transient. Each entity component clears it during `ResetEffects`, and the active `EmotionBuff.Update` method repopulates it later in the tick. Code that needs the resolved emotion should therefore use the normal tModLoader hook order and avoid treating these properties as independently persistent state.

NPCs receive a separate `EmotionNPC` instance through `InstancePerEntity`. Vanilla bosses are immune to emotion changes by default. NPCs derived from `OmoriModNPC` can manage their own immunity rules.

## Applying emotions

### NPC application

Emotion-aware items and projectiles call:

```csharp
EmotionSystem.ApplyEmotion(targetNpc, EmotionType.Angry, durationInTicks);
```

NPC application succeeds only when:

- the requested emotion is not `None`;
- the NPC is not immune to emotion changes;
- a standard tier-one buff is registered for the emotion; and
- the new buff is compatible with the NPC's active emotion buffs.

NPC application currently adds tier one only. It does not promote an already emotional NPC.

### Player application and promotion

Emotion-granting items use a family base type:

```csharp
EmotionSystem.ApplyOrPromoteEmotion<AngryEmotionBase>(
    player,
    EmotionStatTuning.EmotionTimeInSeconds * 60);
```

The operation removes incompatible emotions, then follows one of three paths:

1. If no matching family is active, apply its standard tier-one buff.
2. If a non-final standard tier is active, replace it with the next tier.
3. If promotion is blocked or the final tier is active, reapply the current buff to refresh it.

Normal emotion items pass `canPromoteToFinalTier: false`. They can reach the penultimate tier but cannot cross into the final tier. `EmotionalAmplifier` calls `ApplyFinalTierEmotion`, which promotes a penultimate standard emotion to the final tier or reapplies an existing final-tier emotion.

No-time variants cannot participate in this promotion path.

## Final-tier scaling

Final-tier scaling separates the number of concrete buff classes from the number of balance levels.

`EmotionPlayer` retains:

- `ScalingEmotion`, the family being scaled; and
- `ScalingEmotionLevel`, the current effective level.

When a final-tier buff becomes active, `EnsureScalingEmotion` initializes the scaling level to at least the registered final tier. Reapplying that buff increments the level by one until `EmotionStatTuning.PlayerMaxEmotionLevel` is reached. The same buff type remains active throughout this process.

Changing families or losing the final-tier buff resets the retained scaling state. Tooltips use the effective scaling level and append the level reached beyond the final tier.

Final-tier scaling affects stat calculations, but it does not increase emotional-advantage tier strength. This prevents repeated applications from turning a single tier-4 buff into an unbounded advantage-tier value.

## Emotional advantage

`EmotionSystem.CalculateAdvantage` first checks the emotion triangle. Matching emotions, `None`, and any other non-winning pairing return zero.

When one side wins, magnitude is calculated from registered buff tiers:

```text
advantage magnitude = abs(attacker tier - defender tier) + 1
```

The sign describes the winner:

- positive: the attacker has advantage;
- negative: the defender has advantage;
- zero: neither side has advantage.

The triangle always determines direction. A tier difference changes strength but never reverses which emotion wins.

The signed value modifies source damage:

```text
source damage adjustment = EmotionalAdvantageValuePerLevel * signed advantage
```

With the current `0.07f` tuning value, each advantage level changes source damage by seven percent.

## Combat integration

`EmotionNPC` routes Terraria combat hooks into `EmotionSystem.ApplyCombatModifiers` for:

- player item against NPC;
- player-owned projectile against NPC;
- NPC against NPC; and
- NPC against player.

`EmotionItem.ModifyHitPvp` and `EmotionProjectile.ModifyHitPlayer` cover player-versus-player paths.

Combat resolution applies emotional advantage first, then dispatches to the attacker's active buff. The overload using `NPC.HitModifiers` handles attacks against NPCs, while the overload using `Player.HurtModifiers` handles attacks against players.

Player incoming-damage and post-hurt effects use `EmotionPlayer.ModifyHurt` and `EmotionPlayer.OnHurt`. Sad uses these hooks to reduce health damage and subtract the converted share from mana after the hit.

## Stat scaling

`EmotionStatTuning` is the source of truth for balance values. `EmotionStatScaling` stores:

- `MaximumPercent`: the cap;
- `RatePercent`: the per-level increase during the early phase; and
- `StartingPercent`: the flat value added to the curve.

The family base classes currently use a two-phase curve with a default transition after level three. For level `L`, transition level `C`, rate `R`, maximum `M`, starting value `S`, and maximum emotion level `Lmax`:

```text
if L <= C:
    raw = L * R
else:
    raw = lerp(C * R, M, (L - C) / (Lmax - C))

result = min(raw + S, M) / 100
```

The returned value is a decimal modifier. For example, `0.25` represents 25 percent.

All balance changes should begin in `EmotionStatTuning`. Behavior changes—such as changing which stat is modified or which hook performs the modification—belong in the relevant emotion-family base class.

## Compatibility and removal

`EmotionBuff.IsIncompatibleWith` defines coexistence rules. Its default implementation rejects buffs from different emotion families, making Angry, Happy, and Sad mutually exclusive while allowing buffs from the same family to coexist long enough for promotion logic to replace or refresh them.

The main removal APIs are:

- `ClearAllEmotions`, which removes every emotion buff from an entity; and
- `RemoveIncompatibleEmotions<T>`, which preserves the requested family and removes incompatible emotion buffs.

Player buffs are cleared directly. NPC removal uses direct deletion in single-player or on a dedicated server and requests network-aware removal from a multiplayer client.

## Items, projectiles, and visuals

`EmotionItem` and `EmotionProjectile` apply their declared emotion to an NPC after a successful hit. Both provide an additional emotion-specific hit hook so subclasses can add behavior without replacing the base emotion application.

Their Angry, Happy, and Sad subclasses set the emotion identity in one place. Concrete weapons and ammunition should derive from the appropriate typed base whenever possible.

Emotion buffs emit `EmotionDust` around players. The concrete tier controls `_dustSpawnFrequency`, while the family base class selects red, yellow, or blue. `EmotionNPC` provides a separate visual treatment by pulsing an emotional NPC's tint toward the family color and restoring its original color after the emotion ends.

## Extending the system

### Add a new tier to an existing emotion

1. Create a concrete buff derived from the appropriate family base class.
2. Assign the next contiguous `EmotionTier` in its constructor.
3. Set `_dustSpawnFrequency`.
4. Add localization and texture assets required by tModLoader.
5. Keep the tier at or below `EmotionStatTuning.PlayerMaxEmotionLevel`.
6. Build and load the mod so registry validation can confirm the progression.

No central tier dictionary needs to be edited; the registry discovers the new buff automatically.

### Add a persistent variant

1. Derive from the appropriate family base class.
2. Declare the tier.
3. Set `Main.buffNoTimeDisplay[Type] = true` in `SetStaticDefaults`.
4. Apply it from a persistent source, usually for a short duration every tick.

Remember that no-time variants are intentionally excluded from standard promotion and maximum-tier calculations.

### Add a new emotion family

Adding a family is a broader change:

1. Add the identity to `EmotionType`.
2. Create a family base class derived from `EmotionBuff`.
3. Add player and NPC tuning values to `EmotionStatTuning`.
4. Create a contiguous set of concrete standard tier buffs.
5. Decide its relationships in `EmotionSystem.CheckForAdvantage`.
6. Add typed item and projectile bases if the family needs on-hit content.
7. Add visual colors, tooltip behavior, localization, and textures.
8. Verify every combat pairing, compatibility behavior, and multiplayer removal path.

The current advantage model is a three-member cycle. A fourth emotion requires an explicit design for ties and winning relationships rather than only another enum member.

## Public API guidance

- Use `GetEmotionType(Entity)` when the tModLoader **buff type ID** is needed. Despite its historical name, it does not return `EmotionType`.
- Use the entity's `Emotion` property when the emotion family is needed.
- Use `GetEmotionTier(IEmotionEntity)` for progression and advantage calculations.
- Use `GetEmotionLevel(IEmotionEntity)` for stat scaling.
- Use registry-backed lookup methods instead of hard-coding concrete buff type IDs.
- Pass durations in ticks to application methods. Convert seconds using Terraria's 60-ticks-per-second rate where appropriate.
- Call the matching `Can...` method from item eligibility hooks before calling an application or promotion method from the use hook.

## Verification checklist

After changing the system:

1. Build the mod and confirm registry validation succeeds.
2. Apply each tier in sequence and confirm incompatible families are removed.
3. Confirm normal items stop before the final tier and the amplifier crosses into it.
4. Reapply the final tier and confirm scaling level, tooltip text, and cap behavior.
5. Test all three advantage pairings in both attacker directions.
6. Test player-to-NPC, NPC-to-player, NPC-to-NPC, and PvP combat paths.
7. Test a vanilla boss, a normal vanilla NPC, and an `OmoriModNPC` for immunity behavior.
8. Test no-time accessory emotions and ensure they do not promote.
9. Repeat NPC buff application and removal in multiplayer when networking behavior changes.
