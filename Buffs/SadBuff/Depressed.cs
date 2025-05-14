using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.SadBuff
{
    public class Depressed : SadEmotionBase
    {
        Depressed()
        {
            emotionLevel = 2;

            // player changes
            playerPercentDefenseIncrease = 0.25f;
            playerPercentMovementSpeedDecrease = 0.15f;

            // damage to mana damage
            percentDamageToManaDamageConversion = 0.15f;

            // npc changes
            NPCMaximumDefenseIncreaseThreshold = 2;
            NPCPercentDefenseIncrease = 0.25f;
            NPCPercentMovementSpeedDecrease = 0.2f;    
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Miserable>();
        }
    }
}
