using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours;
using OmoriMod.Content.NPCs.StateManagement;
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
            NPC.boss = true;
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

            behaviourManager = new BehaviourManager();
            behaviourManager.AddBehaviour(new ChasePlayerUnrelenting(_frames, 1));
            behaviourManager.AddBehaviour(new ChasePlayerJump(_frames, 0));
        }

        public override void AI()
        {
            behaviourManager.PerformAIViaExitStatus(this);
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
