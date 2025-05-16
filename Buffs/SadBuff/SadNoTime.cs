using Terraria;
using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.SadBuff
{
    public class SadNoTime : SadEmotionBase
    {
        SadNoTime()
        {
            emotionLevel = 1;
            dustSpawnFrequency = 1;
        }
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}
