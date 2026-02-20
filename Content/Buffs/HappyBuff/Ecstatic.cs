using Terraria.ModLoader;
using OmoriMod.Content.Buffs.Abstract;

namespace OmoriMod.Content.Buffs.HappyBuff
{
    public class Ecstatic : HappyEmotionBase
    {
        Ecstatic()
        {
            emotionLevel = 2;
            dustSpawnFrequency = 2;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Manic>();
        }
    }
}
