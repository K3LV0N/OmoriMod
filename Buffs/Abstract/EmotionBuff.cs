﻿using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.NPCs.Global;
using OmoriMod.Players;
using OmoriMod.Systems.EmotionSystem;
using System;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.Abstract
{
    public abstract class EmotionBuff : ModBuff, IEmotionObject
    {
        public EmotionType Emotion { get; protected set; }

        public int emotionLevel;

        /// <summary>
        /// How often dust spawns for this <see cref="EmotionBuff"/>. A value of 2 means 2 dust is spawned every second.
        /// </summary>
        protected int dustSpawnFrequency;

        public int? nextStageEmotionType;

        protected Color dustColor;

        public virtual void UpdateTier4EmotionBuff(Player player, ref int buffIndex) { }
        public virtual void UpdateEmotionBuff(Player player, ref int buffIndex) { }
        public virtual void UpdateEmotionBuff(NPC npc, ref int buffIndex) { }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EmotionPlayer>().Emotion = Emotion;
            DustHandler(player, ref buffIndex);
            UpdateEmotionBuff(player, ref buffIndex);
            UpdateTier4EmotionBuff(player, ref buffIndex);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EmotionNPC>().Emotion = Emotion;
            UpdateEmotionBuff(npc, ref buffIndex);
        }

        protected float ExponentialGrowthPerLevel(float perLvl, float startingValue = 0)
        {
            // turn values into percents
            float percentPerLvl = perLvl / 100;
            float percentStartingValue = startingValue / 100;

            // add 1 to account for base percent
            float baseMultiplier = (1 + percentPerLvl);
            // remove 1 to isolate growth
            float growth = MathF.Pow(baseMultiplier, emotionLevel) - 1;

            return growth + percentStartingValue;
        }


        /// <param name="emotionMidLevel">The value of <see cref="emotionLevel"/> which will result in the function outputting <paramref name="maxValue"/> + <paramref name="minValue"/> / 2</param>
        protected float LogisticGrowthPerLevel(float perLvl, float maxValue, float emotionMidLevel, float minValue = 0f)
        {
            float percentMaxValue = maxValue / 100;
            float percentMinValue = minValue / 100;
            float percentPerLvl = perLvl / 100;

            float range = percentMaxValue - percentMinValue;

            float exponent = -percentPerLvl * (emotionLevel - emotionMidLevel);
            float value = range / (1 + MathF.Exp(exponent));

            return value + percentMinValue;
        }

        protected float LinearPerLevel(float perLvl, float startingValue = 0)
        {
            // turn values into percents
            float percentPerLvl = perLvl / 100;
            float percentStartingValue = startingValue / 100;

            return (percentPerLvl * emotionLevel) + percentStartingValue;
        }

        private void DustHandler(Player player, ref int buffIndex)
        {
            int dustFrequency = (int)(60 / dustSpawnFrequency);

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


        protected virtual void Tier4ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            string buffTip = $" Level: {emotionLevel - 3}";
            tip += buffTip;
        }
    }
}
