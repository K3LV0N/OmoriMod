using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Angry : AngryEmotionBase
    {
        Angry()
        {
            emotionLevel = 1;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Enraged>();
        }
    }
}
