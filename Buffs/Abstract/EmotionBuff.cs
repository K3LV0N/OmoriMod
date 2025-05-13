using Microsoft.Xna.Framework;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Dusts;
using OmoriMod.Players;
using OmoriMod.Systems.EmotionSystem;
using OmoriMod.Systems.EmotionSystem.Interfaces;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.Abstract
{
    public abstract class EmotionBuff : ModBuff, IEmotionObject
    {
        public EmotionType Emotion { get; protected set; }

        readonly public int maxEmotionLevel = 3;
        public int emotionLevel;

        protected List<int> emotions;

        public Color dustColor;

        public virtual void UpdateEmotionBuff(Player player, ref int buffIndex) { }
        public virtual void UpdateEmotionBuff(NPC npc, ref int buffIndex) { }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EmotionPlayer>().Emotion = Emotion;
            DustHandler(player, ref buffIndex);
            UpdateEmotionBuff(player, ref buffIndex);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EmotionNPC>().Emotion = Emotion;
            UpdateEmotionBuff(npc, ref buffIndex);
        }

        public void DustHandler(Player player, ref int buffIndex)
        {
            int dustSpawningRate = (maxEmotionLevel + 1) - emotionLevel;

            if (player.buffTime[buffIndex] % dustSpawningRate == 0)
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

        // TODO: Look this over to see if I can get this to work. It works for 2 stages
        // then breaks
        /*
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            // Upgrade emotion level
            if (emotionLevel != maxEmotionLevel)
            {
                player.DelBuff(buffIndex);
                player.AddBuff(
                    type: emotions[emotionLevel],
                    timeToAdd: time
                    );
                return true;
            }
            return false;
        }
        */
    }
}
