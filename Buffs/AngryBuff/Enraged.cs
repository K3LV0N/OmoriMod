using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Enraged : AngryEmotionBase
    { 
        Enraged()
        {
            emotionLevel = 2;
            dustSpawnFrequency = 2;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Furious>();
        }
    }
}
