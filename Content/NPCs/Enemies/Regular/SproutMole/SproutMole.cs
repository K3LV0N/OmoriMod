using Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using OmoriMod.Content.Items.BuffItems;
using OmoriMod.Content.Items.Health;
using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management;
using OmoriMod.Content.NPCs.Enemies.General_Behaviours;
using OmoriMod.Content.NPCs.Enemies.General_Behaviours.Chase_Player;
using OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole
{
    public class SproutMole : OmoriBehaviourNPC
    {
        private const int _frames = 9;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = _frames;
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

            behaviourManager = new BehaviourManager(this, _frames);
            behaviourManager.AddBehaviour(new IdleWander(1));
            behaviourManager.AddBehaviour(new SuprisedJump(2));
            behaviourManager.AddBehaviour(new SproutMoleChaseBehaviour(3, 0));
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
            behaviourManager.PerformAIViaExitStatus();
        }


        public override void FindFrame(int frameHeight)
        {
            behaviourManager.PerformFindFrame(frameHeight);
        }
    }
}
