using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Manic : HappyEmotionBase
    {
        Manic()
        {
            emotionLevel = 3;
            playerPercentMovementSpeedIncrease = 0.2f;
        }
    }
}
