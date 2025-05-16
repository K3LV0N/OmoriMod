using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Happy : HappyEmotionBase
    {
        Happy()
        {
            emotionLevel = 1;
            dustSpawnFrequency = 1;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Ecstatic>();
        }
    }
}
