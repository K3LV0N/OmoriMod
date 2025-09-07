using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.Enemies.General_Behaviours.Chase_Player;
using OmoriMod.Content.NPCs.State_Management;
using Terraria;
using Terraria.ID;

namespace OmoriMod.Content.NPCs.Enemies.Bosses.YeOldSprout
{
    public class SproutMoleMinion : OmoriBehaviourNPC
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
            NPC.lifeMax = 30;

            NPC.damage = 6;
            NPC.defense = 2;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath9;

            NPC.value = 0f;
            NPC.knockBackResist = 1f;
            NPC.aiStyle = -1;

            behaviourManager = new BehaviourManager(this, _frames);
            behaviourManager.AddBehaviour(new ChasePlayer(1,
                speed: 1.5f,
                inertia: 20f
                ));
            behaviourManager.AddBehaviour(new ChasePlayerJump(0));
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
