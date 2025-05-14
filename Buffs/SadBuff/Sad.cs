using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.SadBuff
{
    public class Sad : SadEmotionBase
    {
        Sad()
        {
            emotionLevel = 1;

            // player changes
            playerPercentDefenseIncrease = 0.15f;
            playerPercentMovementSpeedDecrease = 0.1f;

            // damage to mana damage
            percentDamageToManaDamageConversion = 0.1f;

            // npc changes
            NPCMaximumDefenseIncreaseThreshold = 2;
            NPCPercentDefenseIncrease = 0.25f;
            NPCPercentMovementSpeedDecrease = 0.2f; 
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Depressed>();
        }
    }
}
