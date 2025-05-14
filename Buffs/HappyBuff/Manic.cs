using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Manic : HappyEmotionBase
    {
        Manic()
        {
            emotionLevel = 3;

            // player changes
            playerPercentMovementSpeedIncrease = 0.2f;
            playerPercentExtraCritChance = 0.35f;
            playerPercentMissChance = 0.2f;

            // npc changes
            NPCPercentMovementSpeedIncrease = 0.2f;
            NPCPercentExtraCritChance = 0.07f;
            NPCPercentMissChance = 0.05f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = null;
        }
    }
}
