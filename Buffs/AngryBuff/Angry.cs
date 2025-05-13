using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

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

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Enraged>();
        }
    }
}
