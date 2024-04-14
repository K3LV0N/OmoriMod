using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Buffs.HappyBuff;

namespace OmoriMod.Buffs.AngryBuff
{
    public class AngryNoTime : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.ClearBuff(ModContent.BuffType<Happy>());
            //player.ClearBuff(ModContent.BuffType<Happy2>());
            player.ClearBuff(ModContent.BuffType<Ecstatic>());
            player.ClearBuff(ModContent.BuffType<Manic>());

            player.ClearBuff(ModContent.BuffType<Sad>());
            //player.ClearBuff(ModContent.BuffType<Sad2>());
            player.ClearBuff(ModContent.BuffType<Depressed>());
            player.ClearBuff(ModContent.BuffType<Miserable>());

            player.statDefense -= (int)(player.statDefense * .125);
        }
    }
}
