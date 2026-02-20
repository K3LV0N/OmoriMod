using Terraria.ModLoader;
using OmoriMod.Content.Buffs.Abstract;

namespace OmoriMod.Content.Buffs.HappyBuff
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
