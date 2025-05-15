using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.Systems.EmotionSystem;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Players
{
    public class EmotionPlayer : ModPlayer, IEmotionEntity
    {
        public EmotionType Emotion { get; set; }

        public bool ImmuneToEmotionChange => false;

        public override void ResetEffects()
        {
            Emotion = EmotionType.NONE;
        }

        public override void PreUpdateBuffs()
        {
            // Remove dummy buff
            Player.ClearBuff(ModContent.BuffType<DummyBuff>());
        }
    }
}
