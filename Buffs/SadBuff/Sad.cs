using Microsoft.Xna.Framework;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Buffs.SadBuff
{
    public class Sad : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {

            player.ClearBuff(ModContent.BuffType<Angry>());
            player.ClearBuff(ModContent.BuffType<Enraged>());
            player.ClearBuff(ModContent.BuffType<Furious>());

            player.ClearBuff(ModContent.BuffType<Happy>());
            player.ClearBuff(ModContent.BuffType<Ecstatic>());
            player.ClearBuff(ModContent.BuffType<Manic>());

            if (player.buffTime[buffIndex] % 3 == 0)
            {
                Dust.NewDust(player.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Blue);
            }

            player.statDefense += (int)(player.statDefense * .25);

            player.moveSpeed *= 0.9f;

        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.RequestBuffRemoval(ModContent.BuffType<Angry>());
            npc.RequestBuffRemoval(ModContent.BuffType<Happy>());
            //Dust.NewDust(npc.Center, 2, 2, DustID.Water, 0f, 0f, 0, Color.Blue);

            if (npc.defense <= npc.defDefense * 3)
            {
                npc.defense = (int)(npc.defDefense * 1.25);
            }
            
        }
    }
}
