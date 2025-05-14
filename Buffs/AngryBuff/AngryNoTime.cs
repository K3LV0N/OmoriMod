using Terraria;
using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.AngryBuff
{
    public class AngryNoTime : AngryEmotionBase
    {
        AngryNoTime()
        {
            emotionLevel = 1;
        }
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}
