using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.SadBuff
{
    public class Sad : SadEmotionBase
    {
        Sad()
        {
            emotionLevel = 1;
            playerPercentMovementSpeedDecrease = 0.1f;
            playerPercentDefenseIncrease = 0.15f;
            NPCMaximumDefenseIncreaseThreshold = 2;
            NPCPercentDefenseIncrease = 0.25f;
        }
    }
}
