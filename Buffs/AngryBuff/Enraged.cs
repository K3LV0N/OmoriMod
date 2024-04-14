using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Dusts;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Enraged : ModBuff
    { 

        public override void Update(Player player, ref int buffIndex)
        {
            player.ClearBuff(ModContent.BuffType<Angry>());

            player.ClearBuff(ModContent.BuffType<Happy>());
            player.ClearBuff(ModContent.BuffType<Ecstatic>());
            player.ClearBuff(ModContent.BuffType<Manic>());

            player.ClearBuff(ModContent.BuffType<Sad>());
            player.ClearBuff(ModContent.BuffType<Depressed>());
            player.ClearBuff(ModContent.BuffType<Miserable>());
            if (player.buffTime[buffIndex] % 2 == 0)
            {
                Dust.NewDust(player.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Red, 1.2f);
            }

            player.statDefense -= (int)(player.statDefense * .25);
        }
    }
}
