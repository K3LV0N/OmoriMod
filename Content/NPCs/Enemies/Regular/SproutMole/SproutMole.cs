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

            behaviourManager = new BehaviourManager();
            behaviourManager.AddBehaviour(new IdleWander(_frames,  1));
            behaviourManager.AddBehaviour(new SuprisedJump(_frames, 2));
            behaviourManager.AddBehaviour(new ChasePlayer(_frames, 3, 0));
            behaviourManager.AddBehaviour(new ChasePlayerJump(_frames, 2));
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
            behaviourManager.PerformAIViaExitStatus(this);
        }


        public override void FindFrame(int frameHeight)
        {
            behaviourManager.PerformFindFrame(frameHeight);
        }
    }
}
