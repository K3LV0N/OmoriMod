using Terraria;
using OmoriMod.Content.Buffs.Abstract;

namespace OmoriMod.Content.Buffs.HappyBuff
{
    public class HappyNoTime : HappyEmotionBase
    {
        HappyNoTime()
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
