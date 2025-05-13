using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.SadBuff
{
    public class Depressed : SadEmotionBase
    {
        Depressed()
        {
            emotionLevel = 2;
            playerPercentMovementSpeedDecrease = 0.15f;
            playerPercentDefenseIncrease = 0.25f;
        }
    }
}
