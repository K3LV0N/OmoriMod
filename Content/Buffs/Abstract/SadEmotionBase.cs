using System;

using Microsoft.Xna.Framework;

using OmoriMod.Content.NPCs.Global;
using OmoriMod.Content.Players;
using OmoriMod.Content.Systems.EmotionSystem;

using Terraria;

namespace OmoriMod.Content.Buffs.Abstract;

/// <summary>
/// Only Defense and Player speed changes here. Damage conversion and NPC speed changes accounted for in <see cref="EmotionNPC"/>
/// </summary>
public abstract class SadEmotionBase : EmotionBuff
{

    // Player Configuration

    // ===== Movement Speed Decrease =====
    private const float PLAYER_MOVEMENT_SPEED_DECREASE_MAX = 80.0f;
    private const float PLAYER_MOVEMENT_SPEED_DECREASE_RATE = 5.0f;
    private const float PLAYER_MOVEMENT_SPEED_DECREASE_STARTING_VALUE = 6.0f;

    // ===== Defense Up =====
    private const float PLAYER_DEFENSE_INCREASE_MAX = 60.0f;
    private const float PLAYER_DEFENSE_INCREASE_RATE = 6.0f;
    private const float PLAYER_DEFENSE_INCREASE_STARTING_VALUE = 3.5f;

    // ===== Damage to Mana Damage =====
    private const float HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_MAX = 75.0f;
    private const float HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_RATE = 6.5f;
    private const float HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_STARTING_VALUE = 6.0f;




    // NPC Configuration

    // ===== Movement Speed Decrease =====
    private const float NPC_DEFENSE_INCREASE_MAX = 50.0f;
    private const float NPC_DEFENSE_INCREASE_RATE = 3.5f;
    private const float NPC_DEFENSE_INCREASE_STARTING_VALUE = 8.5f;

    // ===== Defense Up =====
    private const float NPC_MOVEMENT_SPEED_DECREASE_MAX = 60.0f;
    private const float NPC_MOVEMENT_SPEED_DECREASE_RATE = 4.0f;
    private const float NPC_MOVEMENT_SPEED_DECREASE_STARTING_VALUE = 7.0f;





    // defense up
    public float GetPlayerDefenseIncreasePercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: PLAYER_DEFENSE_INCREASE_MAX,
        rate: PLAYER_DEFENSE_INCREASE_RATE,
        maxEmotionLevel: EmotionSystem.PLAYER_MAX_EMOTION_LEVEL,
        startingValue: PLAYER_DEFENSE_INCREASE_STARTING_VALUE
        );
    public float GetNPCDefenseIncreasePercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: NPC_DEFENSE_INCREASE_MAX,
        rate: NPC_DEFENSE_INCREASE_RATE,
        maxEmotionLevel: EmotionSystem.NPC_MAX_EMOTION_LEVEL,
        startingValue: NPC_DEFENSE_INCREASE_STARTING_VALUE
        );

    // movement speed down
    public float GetPlayerMovementSpeedDecreasePercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: PLAYER_MOVEMENT_SPEED_DECREASE_MAX,
        rate: PLAYER_MOVEMENT_SPEED_DECREASE_RATE,
        maxEmotionLevel: EmotionSystem.PLAYER_MAX_EMOTION_LEVEL,
        startingValue: PLAYER_MOVEMENT_SPEED_DECREASE_STARTING_VALUE
        );
    public float GetNPCMovementSpeedDecreasePercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: NPC_MOVEMENT_SPEED_DECREASE_MAX,
        rate: NPC_MOVEMENT_SPEED_DECREASE_RATE,
        maxEmotionLevel: EmotionSystem.NPC_MAX_EMOTION_LEVEL,
        startingValue: NPC_MOVEMENT_SPEED_DECREASE_STARTING_VALUE
        );

    // damage to mana damage
    public float GetHealthDamageToManaDamageConversionPercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_MAX,
        rate: HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_RATE,
        maxEmotionLevel: EmotionSystem.PLAYER_MAX_EMOTION_LEVEL,
        startingValue: HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_STARTING_VALUE
        );



    public SadEmotionBase()
    {
        Emotion = EmotionType.SAD;
        dustColor = Color.Blue;
    }

    public override void UpdateEmotionBuff(Player player, ref int buffIndex)
    {
        EmotionSystem.RemoveIncompatibleEmotions<SadEmotionBase>(player);
        int emotionLevel = player.GetModPlayer<EmotionPlayer>().EmotionLevel;
        ModifyPlayerDefense(player, emotionLevel);
        ModifyPlayerMovement(player, emotionLevel); // Sad also reduces speed
    }

    public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
    {
        EmotionSystem.RemoveIncompatibleEmotions<SadEmotionBase>(npc);
        int emotionLevel = npc.GetGlobalNPC<EmotionNPC>().EmotionLevel;
        ModifyNPCDefense(npc, emotionLevel);
        ModifyNPCMovement(npc, emotionLevel);
    }

    public override void ModifyPlayerDefense(Player player, int emotionLevel)
    {
        player.statDefense += (int)(player.statDefense * GetPlayerDefenseIncreasePercent(emotionLevel));
    }

    public override void ModifyPlayerMovement(Player player, int emotionLevel)
    {
        player.moveSpeed *= 1 - GetPlayerMovementSpeedDecreasePercent(emotionLevel);
    }

    public override void ModifyNPCDefense(NPC npc, int emotionLevel)
    {
        int increasedDefense = npc.defDefense * (int)(1 + GetNPCDefenseIncreasePercent(emotionLevel));
        npc.defense = increasedDefense;
    }

    public override void ModifyNPCMovement(NPC npc, int emotionLevel)
    {
        // CalculateNewPosition logic from Helper
        float modifier = -GetNPCMovementSpeedDecreasePercent(emotionLevel);
        Vector2 change;
        if (npc.noGravity) { change = npc.velocity * modifier; }
        else { change = new Vector2(npc.velocity.X * modifier, 0); }
        npc.position += change;
    }

    public override void ModifyPlayerIncomingDamage(int emotionLevel, ref Player.HurtModifiers modifiers)
    {
        modifiers.SourceDamage *= 1 - GetHealthDamageToManaDamageConversionPercent(emotionLevel);
    }

    public override void OnPlayerHurt(Player player, int emotionLevel, Player.HurtInfo hurtInfo)
    {
        float manaChange = hurtInfo.SourceDamage * GetHealthDamageToManaDamageConversionPercent(emotionLevel);
        if ((int)(player.statMana - manaChange) > 0)
        {
            player.statMana = (int)(player.statMana - manaChange);
        }
        else
        {
            player.statMana = 0;
        }
    }

    public virtual void SadModifyBuffText(ref string buffName, ref string tip, ref int rare) { }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        int emotionLevel = GetTooltipEmotionLevel();
        int defenseUp = (int)MathF.Round(GetPlayerDefenseIncreasePercent(emotionLevel) * 100);
        int speedDown = (int)MathF.Round(GetPlayerMovementSpeedDecreasePercent(emotionLevel) * 100);
        int mana = (int)MathF.Round(GetHealthDamageToManaDamageConversionPercent(emotionLevel) * 100);
        string buffTip = $"Defense up by {defenseUp}%!" +
            $" Speed down by {speedDown}%!" +
            $" {mana}% of damage convertd to mana damage!";
        tip = buffTip;
        SadModifyBuffText(ref buffName, ref tip, ref rare);
        FinalTierModifyBuffText(emotionLevel, ref buffName, ref tip, ref rare);
    }
}
