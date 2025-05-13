using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.SadBuff
{
    public class Miserable : SadEmotionBase
    {
        Miserable()
        {
            emotionLevel = 3;
            playerPercentMovementSpeedDecrease = 0.25f;
            playerPercentDefenseIncrease = 0.4f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = null;
        }
    }
}
