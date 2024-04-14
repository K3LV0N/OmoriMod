using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Dusts;

namespace OmoriMod.Buffs.HappyBuff
{
    public class Happy : ModBuff
    {

        public override void Update(Player player, ref int buffIndex)
        {

            player.ClearBuff(ModContent.BuffType<Angry>());
            player.ClearBuff(ModContent.BuffType<Enraged>());
            player.ClearBuff(ModContent.BuffType<Furious>());

            player.ClearBuff(ModContent.BuffType<Sad>());
            player.ClearBuff(ModContent.BuffType<Depressed>());
            player.ClearBuff(ModContent.BuffType<Miserable>());
            if (player.buffTime[buffIndex] % 3 == 0)
            {
                Dust.NewDust(player.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Yellow);
            }

            player.moveSpeed *= 1.20f;

        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.RequestBuffRemoval(ModContent.BuffType<Sad>());
            npc.RequestBuffRemoval(ModContent.BuffType<Angry>());
            //Dust.NewDust(npc.Center, 2, 2, DustID.Pixie, 0f, 0f, 0, Color.Yellow);
        }
    }
}
