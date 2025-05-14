using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Enraged : AngryEmotionBase
    { 
        Enraged()
        {
            emotionLevel = 2;

            // player changes
            playerPercentDamageIncrease = 0.20f;
            playerPercentDefenseDecrease = 0.25f;

            // npc changes
            NPCPercentDamageIncrease = 0.12f;
            NPCMinimumDefenseIncreaseThreshold = 0.40f;
            NPCPercentDefenseDecrease = 0.2f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Furious>();
        }
    }
}
