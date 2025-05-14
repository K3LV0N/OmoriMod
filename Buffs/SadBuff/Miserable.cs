using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.SadBuff
{
    public class Miserable : SadEmotionBase
    {
        Miserable()
        {
            emotionLevel = 3;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = null;
        }
    }
}
