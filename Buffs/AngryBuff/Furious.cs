using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Furious : AngryEmotionBase
    {
        Furious()
        {
            emotionLevel = 3;

            // player changes
            playerPercentDamageIncrease = 0.33f;
            playerPercentDefenseDecrease = 0.4f;

            // npc changes
            NPCPercentDamageIncrease = 0.12f;
            NPCMinimumDefenseIncreaseThreshold = 0.40f;
            NPCPercentDefenseDecrease = 0.2f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = null;
        }
    }
}
