using System;

using Microsoft.Xna.Framework;

using OmoriMod.Content.Dusts;
using OmoriMod.Content.NPCs.Global;
using OmoriMod.Content.Players;
using OmoriMod.Content.Systems.EmotionSystem;

using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Content.Buffs.Abstract;

public abstract class EmotionBuff : ModBuff, IEmotionObject
{
    public EmotionType Emotion { get; protected set; }

    /// <summary>
    /// The tier declared by this buff type. This is configuration data shared by every
    /// entity using the buff and must not contain runtime player or NPC state.
    /// </summary>
    public int EmotionTier { get; protected set; }

    /// <summary>
    /// How often dust spawns for this <see cref="EmotionBuff"/>. A value of 2 means 2 dust is spawned every second.
    /// </summary>
    protected int dustSpawnFrequency;

    public virtual bool IsIncompatibleWith(EmotionBuff otherBuff)
    {
        return Emotion != otherBuff.Emotion;
    }

    protected Color dustColor;

    public virtual void UpdateEmotionBuff(Player player, ref int buffIndex) { }
    public virtual void UpdateEmotionBuff(NPC npc, ref int buffIndex) { }

    public override void Update(Player player, ref int buffIndex)
    {
        var modPlayer = player.GetModPlayer<EmotionPlayer>();
        modPlayer.Emotion = Emotion;
        modPlayer.ActiveEmotionBuff = this;

        UpdateEmotionLevel(modPlayer);
        DustHandler(player, ref buffIndex);
        UpdateEmotionBuff(player, ref buffIndex);
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        var emotionNPC = npc.GetGlobalNPC<EmotionNPC>();
        emotionNPC.Emotion = Emotion;
        emotionNPC.ActiveEmotionBuff = this;
        emotionNPC.EmotionLevel = EmotionSystem.GetEmotionTier(Type) ?? EmotionTier;

        UpdateEmotionBuff(npc, ref buffIndex);
    }

    private void UpdateEmotionLevel(EmotionPlayer modPlayer)
    {
        int registeredTier = EmotionSystem.GetEmotionTier(Type) ?? EmotionTier;
        if (!EmotionSystem.IsFinalEmotionTier(Type))
        {
            modPlayer.EmotionLevel = registeredTier;
            return;
        }

        int? finalTier = EmotionSystem.GetMaxEmotionTier(Emotion);
        if (!finalTier.HasValue)
        {
            return;
        }

        modPlayer.EnsureScalingEmotion(Emotion, finalTier.Value);
        modPlayer.EmotionLevel = modPlayer.scalingEmotionLevel;
    }

    public override bool ReApply(Player player, int time, int buffIndex)
    {
        if (!EmotionSystem.IsFinalEmotionTier(Type))
        {
            return base.ReApply(player, time, buffIndex);
        }

        int? finalTier = EmotionSystem.GetMaxEmotionTier(Emotion);
        if (!finalTier.HasValue)
        {
            return base.ReApply(player, time, buffIndex);
        }

        EmotionPlayer modPlayer = player.GetModPlayer<EmotionPlayer>();
        modPlayer.EnsureScalingEmotion(Emotion, finalTier.Value);
        if (modPlayer.scalingEmotionLevel < EmotionSystem.PLAYER_MAX_EMOTION_LEVEL)
        {
            modPlayer.scalingEmotionLevel++;
        }
        modPlayer.EmotionLevel = modPlayer.scalingEmotionLevel;

        return false;
    }

    // Virtual Modifiers
    public virtual void ModifyPlayerDefense(Player player, int emotionLevel) { }
    public virtual void ModifyNPCDefense(NPC npc, int emotionLevel) { }
    public virtual void ModifyPlayerMovement(Player player, int emotionLevel) { }
    public virtual void ModifyNPCMovement(NPC npc, int emotionLevel) { }

    public virtual void ModifyPlayerOutgoingDamage(int emotionLevel, ref NPC.HitModifiers modifiers) { }
    public virtual void ModifyPlayerOutgoingDamage(int emotionLevel, ref Player.HurtModifiers modifiers) { }

    public virtual void ModifyNPCOutgoingDamage(int emotionLevel, ref Player.HurtModifiers modifiers) { }

    // Happy Hit Modifiers (Player attacking NPC)
    public virtual void ModifyPlayerHitNPC(int emotionLevel, ref NPC.HitModifiers modifiers) { }
    // Happy Hit Modifiers (Player attacking Player/Self?)
    public virtual void ModifyPlayerHitPlayer(int emotionLevel, ref Player.HurtModifiers modifiers) { }

    // Sad Damage Reduction (Player taking damage)
    public virtual void ModifyPlayerIncomingDamage(int emotionLevel, ref Player.HurtModifiers modifiers) { }

    // Sad Mana Conversion (NPC hitting Player)
    public virtual void OnPlayerHurt(Player player, int emotionLevel, Player.HurtInfo hurtInfo) { }


    /// <summary>
    /// Calculates a linear rate of change using <paramref name="rate"/> until
    /// <paramref name="emotionLevel"/> = <paramref name="rateChange"/>. Then shifts to linear rate of change using percentage of remaining emotion levels up until
    /// <paramref name="maxEmotionLevel"/> to reach <paramref name="max"/>.
    /// </summary>
    /// <param name="max">The maximum return value of this function. In percentage form.</param>
    /// <param name="rate">The rate of change for the linear function until <paramref name="emotionLevel"/> = <paramref name="rateChange"/>.</param>
    /// <param name="startingValue">The starting value for this function.</param>
    /// <param name="maxEmotionLevel">The maximum value that <paramref name="emotionLevel"/> can be.</param>
    /// <param name="rateChange">The emotion level when the percentage remaining linear function begins.</param>
    /// <returns></returns>
    protected static float LinearPerLevel(int emotionLevel, float max, float rate, int maxEmotionLevel, float startingValue = 0f, int rateChange = 3)
    {
        float result;

        if (emotionLevel <= rateChange)
        {
            // Phase 1: simple linear
            result = emotionLevel * rate;
        }
        else
        {
            // Phase 2: linear interpolation to max
            float initialValue = rateChange * rate;
            float t = (emotionLevel - rateChange) / (float)(maxEmotionLevel - rateChange);
            result = MathHelper.Lerp(initialValue, max, t);
        }

        result += startingValue;
        return Math.Min(result, max) / 100f;
    }


    protected static float ExponentialGrowthPerLevel(int emotionLevel, float perLvl, float startingValue = 0)
    {
        // turn values into percents
        float percentPerLvl = perLvl / 100;
        float percentStartingValue = startingValue / 100;

        // add 1 to account for base percent
        float baseMultiplier = 1 + percentPerLvl;
        // remove 1 to isolate growth
        float growth = MathF.Pow(baseMultiplier, emotionLevel) - 1;

        return growth + percentStartingValue;
    }


    /// <param name="emotionMidLevel">The emotion level which will result in the function outputting <paramref name="maxValue"/> + <paramref name="minValue"/> / 2</param>
    protected static float LogisticGrowthPerLevel(int emotionLevel, float perLvl, float maxValue, float emotionMidLevel, float minValue = 0f)
    {
        float percentMaxValue = maxValue / 100;
        float percentMinValue = minValue / 100;
        float percentPerLvl = perLvl / 100;

        float range = percentMaxValue - percentMinValue;

        float exponent = -percentPerLvl * (emotionLevel - emotionMidLevel);
        float value = range / (1 + MathF.Exp(exponent));

        return value + percentMinValue;
    }

    protected static float LinearPerLevel(int emotionLevel, float perLvl, float startingValue = 0)
    {
        // turn values into percents
        float percentPerLvl = perLvl / 100;
        float percentStartingValue = startingValue / 100;

        return percentPerLvl * emotionLevel + percentStartingValue;
    }

    private void DustHandler(Player player, ref int buffIndex)
    {
        int dustFrequency = 60 / dustSpawnFrequency;

        if (player.buffTime[buffIndex] % dustFrequency == 0)
        {
            Dust.NewDust(
            Position: player.Center,
            Width: 2,
            Height: 2,
            Type: ModContent.DustType<EmotionDust>(),
            SpeedX: 0f,
            SpeedY: 0f,
            Alpha: 0,
            newColor: dustColor
            );
        }
    }


    protected int GetTooltipEmotionLevel()
    {
        int registeredTier = EmotionSystem.GetEmotionTier(Type) ?? EmotionTier;
        Player localPlayer = Main.LocalPlayer;
        if (localPlayer == null || !localPlayer.active || !localPlayer.HasBuff(Type))
        {
            return registeredTier;
        }

        EmotionPlayer emotionPlayer = localPlayer.GetModPlayer<EmotionPlayer>();
        if (emotionPlayer.ActiveEmotionBuff?.Type == Type && emotionPlayer.EmotionLevel > 0)
        {
            return emotionPlayer.EmotionLevel;
        }

        return EmotionSystem.IsFinalEmotionTier(Type)
            && emotionPlayer.scalingEmotion == Emotion
            && emotionPlayer.scalingEmotionLevel >= registeredTier
                ? emotionPlayer.scalingEmotionLevel
                : registeredTier;
    }

    protected void FinalTierModifyBuffText(int emotionLevel, ref string buffName, ref string tip, ref int rare)
    {
        if (!EmotionSystem.IsFinalEmotionTier(Type))
        {
            return;
        }

        int? finalTier = EmotionSystem.GetMaxEmotionTier(Emotion);
        if (finalTier.HasValue)
        {
            tip += $" Level: {emotionLevel - finalTier.Value + 1}";
        }
    }
}
