using Microsoft.Xna.Framework;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.SadBuff
{
    public class Miserable : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.ClearBuff(ModContent.BuffType<Sad>());
            player.ClearBuff(ModContent.BuffType<Depressed>());

            player.ClearBuff(ModContent.BuffType<Angry>());
            player.ClearBuff(ModContent.BuffType<Enraged>());
            player.ClearBuff(ModContent.BuffType<Furious>());

            player.ClearBuff(ModContent.BuffType<Happy>());
            player.ClearBuff(ModContent.BuffType<Ecstatic>());
            player.ClearBuff(ModContent.BuffType<Manic>());
            Dust.NewDust(player.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Blue, 1.5f);

            player.statDefense += player.statDefense;

            player.moveSpeed *= 0.6f;

        }
    }
}
