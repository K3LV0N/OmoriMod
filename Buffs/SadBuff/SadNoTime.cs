using Terraria;
using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.SadBuff
{
    public class SadNoTime : SadEmotionBase
    {
        SadNoTime()
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
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}
