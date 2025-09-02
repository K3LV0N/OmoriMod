﻿using OmoriMod.Content.Buffs.Abstract;
using OmoriMod.Content.Buffs.Abstract.Helpers;
using OmoriMod.Content.Players;
using Terraria;

namespace OmoriMod.Content.Buffs.AngryBuff
{
    public class Livid : AngryEmotionBase
    {
        Livid()
        {
            emotionLevel = 4;
            dustSpawnFrequency = 4;
        }


        public override void UpdateTier4EmotionBuff(Player player, ref int buffIndex)
        {
            emotionLevel = player.GetModPlayer<EmotionPlayer>().tier4EmotionLevel;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if (player.GetModPlayer<EmotionPlayer>().tier4EmotionLevel < EmotionHelper.PLAYER_MAX_EMOTION_LEVEL) player.GetModPlayer<EmotionPlayer>().tier4EmotionLevel++;
            return false;
        }

        public override void AngryModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            Tier4ModifyBuffText(ref buffName, ref tip, ref rare);
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = null;
        }
    }
}