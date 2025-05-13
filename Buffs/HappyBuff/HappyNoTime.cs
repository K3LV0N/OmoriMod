using Terraria;
using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.HappyBuff
{
    public class HappyNoTime : HappyEmotionBase
    {
        HappyNoTime()
        {
            emotionLevel = 1;
            playerPercentMovementSpeedIncrease = 0.1f;
        }
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}
