using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Dusts;

namespace OmoriMod.Buffs.AngryBuff
{
    public class Angry : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {

            player.ClearBuff(ModContent.BuffType<Happy>());
            player.ClearBuff(ModContent.BuffType<Ecstatic>());
            player.ClearBuff(ModContent.BuffType<Manic>());

            player.ClearBuff(ModContent.BuffType<Sad>());
            player.ClearBuff(ModContent.BuffType<Depressed>());
            player.ClearBuff(ModContent.BuffType<Miserable>());
            if (player.buffTime[buffIndex] % 3 == 0)
            {
                Dust.NewDust(player.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Red);
            }

            player.statDefense -= (int)(player.statDefense * .125);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.RequestBuffRemoval(ModContent.BuffType<Sad>());
            npc.RequestBuffRemoval(ModContent.BuffType<Happy>());
            //Dust.NewDust(npc.Center, 2, 2, DustID.Water_BloodMoon, 0f, 0f, 0, Color.Red);

            // Only defense alterations, damage is done when the player is hit (GLOBAL NPC)

            if (npc.defense >= npc.defDefense * 0.35f)
            {
                npc.defense = (int)(npc.defDefense * .75);
            }
        }
    }
}
