using OmoriMod.Buffs.Abstract;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Ecstatic : HappyEmotionBase
    {
        Ecstatic()
        {
            emotionLevel = 2;

            // player changes
            playerPercentMovementSpeedIncrease = 0.15f;
            playerPercentExtraCritChance = 0.2f;
            playerPercentMissChance = 0.15f;

            // npc changes
            NPCPercentMovementSpeedIncrease = 0.2f;
            NPCPercentExtraCritChance = 0.07f;
            NPCPercentMissChance = 0.05f;
        }

        public override void SetStaticDefaults()
        {
            nextStageEmotionType = ModContent.BuffType<Manic>();
        }
    }
}
