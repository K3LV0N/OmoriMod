﻿using Terraria;
using OmoriMod.Content.Buffs.Abstract;

namespace OmoriMod.Content.Buffs.SadBuff
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
