using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Enraged : AngryEmotionBase
    { 
        Enraged()
        {
            emotionLevel = 2;
            playerPercentDefenseDecrease = 0.25f;
        }
    }
}
