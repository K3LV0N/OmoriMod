using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Angry : AngryEmotionBase
    {
        Angry()
        {
            emotionLevel = 1;
            playerPercentDefenseDecrease = 0.125f;
            NPCMinimumDefenseIncreaseThreshold = 0.40f;
            NPCPercentDefenseDecrease = 0.2f;
        }
    }
}
