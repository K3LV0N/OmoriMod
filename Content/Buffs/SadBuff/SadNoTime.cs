using OmoriMod.Content.Buffs.Abstract;

using Terraria;

namespace OmoriMod.Content.Buffs.SadBuff;

public class SadNoTime : SadEmotionBase
{
    SadNoTime()
    {
        EmotionTier = 1;
        dustSpawnFrequency = 1;
    }
    public override void SetStaticDefaults()
    {
        Main.buffNoTimeDisplay[Type] = true;
    }
}
