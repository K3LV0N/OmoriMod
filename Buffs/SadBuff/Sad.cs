using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.SadBuff
{
    public class Sad : SadEmotionBase
    {
        Sad()
        {
            emotionLevel = 1;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Depressed>();
        }
    }
}
