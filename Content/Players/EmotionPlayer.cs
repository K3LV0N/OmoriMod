using OmoriMod.Content.Buffs.Abstract;
using OmoriMod.Content.Systems.EmotionSystem;

using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Content.Players;

public class EmotionPlayer : ModPlayer, IEmotionEntity
{
    public EmotionType Emotion { get; set; }
    public EmotionBuff ActiveEmotionBuff { get; set; }

    public bool ImmuneToEmotionChange => false;
    public int scalingEmotionLevel;
    public EmotionType scalingEmotion;
    public int MidEmotionLevel;

    private void ResetMidEmotionLevel()
    {
        if (Main.hardMode)
        {
            MidEmotionLevel = 10;
        }
        else
        {
            MidEmotionLevel = 6;
        }
    }

    public void EnsureScalingEmotion(EmotionType emotion, int finalTier)
    {
        if (scalingEmotion != emotion || scalingEmotionLevel < finalTier)
        {
            scalingEmotion = emotion;
            scalingEmotionLevel = finalTier;
        }
    }

    private void ResetScalingEmotionLevel()
    {
        int? emotionType = EmotionSystem.GetEmotionType(Player);
        if (!emotionType.HasValue
            || !EmotionSystem.IsFinalEmotionTier(emotionType.Value)
            || ModContent.GetModBuff(emotionType.Value) is not EmotionBuff emotionBuff
            || !EmotionSystem.GetMaxEmotionTier(emotionBuff.Emotion).HasValue)
        {
            scalingEmotion = EmotionType.NONE;
            scalingEmotionLevel = 0;
            return;
        }

        EnsureScalingEmotion(
            emotionBuff.Emotion,
            EmotionSystem.GetMaxEmotionTier(emotionBuff.Emotion).Value);
    }

    public override void ResetEffects()
    {
        Emotion = EmotionType.NONE;
        ActiveEmotionBuff = null;
        ResetMidEmotionLevel();
        ResetScalingEmotionLevel();
    }

    public override void PreUpdateBuffs()
    {
        // Remove dummy buff
        Player.ClearBuff(ModContent.BuffType<DummyBuff>());
    }
}
