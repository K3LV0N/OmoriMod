using Terraria;
using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.AngryBuff
{
    public class AngryNoTime : AngryEmotionBase
    {
        AngryNoTime()
        {
            emotionLevel = 1;

            // player changes
            playerPercentDamageIncrease = 0.15f;
            playerPercentDefenseDecrease = 0.125f;

            // npc changes
            NPCPercentDamageIncrease = 0.12f;
            NPCMinimumDefenseIncreaseThreshold = 0.40f;
            NPCPercentDefenseDecrease = 0.2f;
        }
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}
