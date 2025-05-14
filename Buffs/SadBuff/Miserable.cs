using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.SadBuff
{
    public class Miserable : SadEmotionBase
    {
        Miserable()
        {
            emotionLevel = 3;

            // player changes
            playerPercentDefenseIncrease = 0.4f;
            playerPercentMovementSpeedDecrease = 0.35f;

            // damage to mana damage
            percentDamageToManaDamageConversion = 0.25f;

            // npc changes
            NPCMaximumDefenseIncreaseThreshold = 2;
            NPCPercentDefenseIncrease = 0.25f;
            NPCPercentMovementSpeedDecrease = 0.2f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = null;
        }
    }
}
