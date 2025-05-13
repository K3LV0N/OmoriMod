using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Happy : HappyEmotionBase
    {
        Happy()
        {
            emotionLevel = 1;
            playerPercentMovementSpeedIncrease = 0.1f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Ecstatic>();
        }
    }
}
