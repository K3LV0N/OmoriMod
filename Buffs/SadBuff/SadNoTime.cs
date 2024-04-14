using Microsoft.Xna.Framework;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.SadBuff
{
    public class SadNoTime : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.ClearBuff(ModContent.BuffType<Angry>());
            //player.ClearBuff(ModContent.BuffType<Angry2>());
            player.ClearBuff(ModContent.BuffType<Enraged>());
            player.ClearBuff(ModContent.BuffType<Furious>());

            player.ClearBuff(ModContent.BuffType<Happy>());
            //player.ClearBuff(ModContent.BuffType<Happy2>());
            player.ClearBuff(ModContent.BuffType<Ecstatic>());
            player.ClearBuff(ModContent.BuffType<Manic>());

            player.statDefense += (int)(player.statDefense * .25);

            player.moveSpeed *= 0.9f;

        }
    }
}
