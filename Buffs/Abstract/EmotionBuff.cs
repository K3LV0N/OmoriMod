using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Players;
using OmoriMod.Systems.EmotionSystem;
using OmoriMod.Systems.EmotionSystem.Interfaces;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.Abstract
{
    public abstract class EmotionBuff : ModBuff, IEmotionObject
    {
        public EmotionType Emotion { get; protected set; }

        readonly public int maxEmotionLevel = 3;
        protected int emotionLevel;

        public int? nextStageEmotionType;

        protected Color dustColor;

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

        private void DustHandler(Player player, ref int buffIndex)
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
    }
}
