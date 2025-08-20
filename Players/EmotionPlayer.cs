﻿using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.Systems.EmotionSystem;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Players
{
    public class EmotionPlayer : ModPlayer, IEmotionEntity
    {
        public EmotionType Emotion { get; set; }

        public bool ImmuneToEmotionChange => false;
        public int tier4EmotionLevel;
        public int MidEmotionLevel;

        private void ResetMidEmotionLevel()
        {
            if (Main.hardMode) {
                MidEmotionLevel = 10;
            }
            else
            {
                MidEmotionLevel = 6;
            }
        }

        private void ResetTier4EmotionLevel()
        {
            // only reset tier4EmotionLevel when no buff is there
            int? emotionType = EmotionHelper.GetEmotionType(Player);
            if (!emotionType.HasValue) { tier4EmotionLevel = 4; }
            else if (!EmotionHelper.TIER4_EMOTION_TYPES.Contains(emotionType.Value)) { tier4EmotionLevel = 4; }
        }

        public override void ResetEffects()
        {
            Emotion = EmotionType.NONE;
            ResetMidEmotionLevel();
            ResetTier4EmotionLevel();
        }

        public override void PreUpdateBuffs()
        {
            // Remove dummy buff
            Player.ClearBuff(ModContent.BuffType<DummyBuff>());
        }
    }
}
