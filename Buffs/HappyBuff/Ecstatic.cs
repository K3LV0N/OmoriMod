using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Ecstatic : HappyEmotionBase
    {
        Ecstatic()
        {
            emotionLevel = 2;
            playerPercentMovementSpeedIncrease = 0.15f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Manic>();
        }
    }
}
