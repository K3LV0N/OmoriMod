using Microsoft.Xna.Framework;
using Terraria;
using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Ecstatic : HappyEmotionBase
    {
        Ecstatic()
        {
            emotionLevel = 2;
            playerPercentMovementSpeedIncrease = 0.15f;
        }
    }
}
