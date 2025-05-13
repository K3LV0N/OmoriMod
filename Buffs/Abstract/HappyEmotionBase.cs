using OmoriMod.Systems.EmotionSystem.Interfaces;
using Microsoft.Xna.Framework;
using OmoriMod.Buffs.SadBuff;
using Terraria.ModLoader;
using Terraria;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Systems.EmotionSystem;
using OmoriMod.Buffs.HappyBuff;

namespace OmoriMod.Buffs.Abstract
{
    /// <summary>
    /// Only Player speed changes here. Crits and Misses accounted for in <see cref="EmotionNPC"/>
    /// </summary>
    public abstract class HappyEmotionBase : EmotionBuff
    {
        public float playerPercentMovementSpeedIncrease;
        public HappyEmotionBase() 
        {
            Emotion = EmotionType.HAPPY;
            dustColor = Color.Yellow;

            emotions =
            [
                ModContent.BuffType<Happy>(),
                ModContent.BuffType<Ecstatic>(),
                ModContent.BuffType<Manic>(),
            ];
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {
            player.ClearBuff(ModContent.BuffType<Angry>());
            player.ClearBuff(ModContent.BuffType<Enraged>());
            player.ClearBuff(ModContent.BuffType<Furious>());

            player.ClearBuff(ModContent.BuffType<Sad>());
            player.ClearBuff(ModContent.BuffType<Depressed>());
            player.ClearBuff(ModContent.BuffType<Miserable>());

            player.moveSpeed *= (1 + playerPercentMovementSpeedIncrease);
        }

        public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
        {
            npc.RequestBuffRemoval(ModContent.BuffType<Sad>());
            npc.RequestBuffRemoval(ModContent.BuffType<Angry>());
        }
    }
}
