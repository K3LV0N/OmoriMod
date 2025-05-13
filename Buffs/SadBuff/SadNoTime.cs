using Terraria;
using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.SadBuff
{
    public class SadNoTime : SadEmotionBase
    {
        SadNoTime()
        {
            emotionLevel = 1;
            playerPercentMovementSpeedDecrease = 0.1f;
            playerPercentDefenseIncrease = 0.15f;
            NPCMaximumDefenseIncreaseThreshold = 2;
            NPCPercentDefenseIncrease = 0.25f;
        }
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}
