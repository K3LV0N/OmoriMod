using Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using OmoriMod.Content.Items.BuffItems;
using OmoriMod.Content.Items.Health;
using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole
{
    internal class SproutMole : OmoriBehaviourNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 9;
        }

        public override void SetDefaults()
        {
            NPC.width = 17;
            NPC.height = 30;
            NPC.lifeMax = 40;

            NPC.damage = 10;
            NPC.defense = 4;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath9;

            NPC.value = 10f;
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = -1;
            NPC.netUpdate = true;

            behaviourManager = new BehaviourManager();
            behaviourManager.AddBehaviour(new IdleWander(1));
            behaviourManager.AddBehaviour(new SuprisedJump(2));
            behaviourManager.AddBehaviour(new ChasePlayer(3, 0));
            behaviourManager.AddBehaviour(new ChasePlayerJump(2));
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tofu>(), 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AirHorn>(), 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PartyPopper>(), 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainCloud>(), 5));
        }
        

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnModifier = .4f;
            // good chance of spawning if day on surface and underground / Caverns
            return SpawnCondition.OverworldDaySlime.Chance * spawnModifier + 
                SpawnCondition.Underground.Chance * spawnModifier + 
                SpawnCondition.Cavern.Chance * spawnModifier;
        }

        public override void AI()
        {
            behaviourManager.PerformViaExitStatus(this);
        }

        private const int frame1 = 0;
        private const int frame2 = 1;
        private const int frame3 = 2;
        private const int frame4 = 3;
        private const int frame5 = 4;
        private const int frame6 = 5;
        private const int frame7 = 6;
        private const int frame8 = 7;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;

            if (NPC.frameCounter < 10)
            {
                NPC.frame.Y = frame1 * frameHeight;
            }
            else if (NPC.frameCounter < 20)
            {
                NPC.frame.Y = frame2 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = frame3 * frameHeight;
            }
            else if (NPC.frameCounter < 40)
            {
                NPC.frame.Y = frame4 * frameHeight;
            }
            else if (NPC.frameCounter < 50)
            {
                NPC.frame.Y = frame5 * frameHeight;
            }
            else if (NPC.frameCounter < 60)
            {
                NPC.frame.Y = frame6 * frameHeight;
            }
            else if (NPC.frameCounter < 70)
            {
                NPC.frame.Y = frame7 * frameHeight;
            }
            else if (NPC.frameCounter < 80)
            {
                NPC.frame.Y = frame8 * frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }

        }
    }
}
