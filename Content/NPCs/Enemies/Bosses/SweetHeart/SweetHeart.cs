﻿using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Systems;
using OmoriMod.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Content.NPCs.Enemies.Bosses.SweetHeart
{
    [AutoloadBossHead]
    public class SweetHeart : OmoriBossEnemy
    {

        public SweetHeart()
        {
            bossName = "SweetHeart".OmoriModString();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
        }
        public override void SetDefaultsBossEnemy()
        {
            NPC.width = 40;
            NPC.height = 82;
            NPC.lifeMax = 10000;

            NPC.damage = 60;
            NPC.defense = 30;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath9;

            NPC.value = 10000f;
            NPC.knockBackResist = 0.05f;
            NPC.aiStyle = NPCAIStyleID.Slime;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            
        }


        public override void OnKill()
        {
            DownedBossSystem.MarkDowned(bossName);
        }
    }
}
