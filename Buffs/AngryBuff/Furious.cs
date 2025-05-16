using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Furious : AngryEmotionBase
    {
        Furious()
        {
            emotionLevel = 3;
            dustSpawnFrequency = 3;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = null;
        }
    }
}
