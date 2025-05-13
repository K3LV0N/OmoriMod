using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Enraged : AngryEmotionBase
    { 
        Enraged()
        {
            emotionLevel = 2;
            playerPercentDefenseDecrease = 0.25f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Furious>();
        }
    }
}
