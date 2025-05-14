using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Happy : HappyEmotionBase
    {
        Happy()
        {
            emotionLevel = 1;

            // player changes
            playerPercentMovementSpeedIncrease = 0.1f;
            playerPercentExtraCritChance = 0.12f;
            playerPercentMissChance = 0.12f;

            // npc changes
            NPCPercentMovementSpeedIncrease = 0.2f;
            NPCPercentExtraCritChance = 0.07f;
            NPCPercentMissChance = 0.05f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Ecstatic>();
        }
    }
}
