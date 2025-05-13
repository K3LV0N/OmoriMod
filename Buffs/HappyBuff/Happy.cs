using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Happy : HappyEmotionBase
    {
        Happy()
        {
            emotionLevel = 1;
            playerPercentMovementSpeedIncrease = 0.1f;
        }
    }
}
