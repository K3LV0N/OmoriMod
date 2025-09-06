using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Content.NPCs.StateManagement.NPCBehaviours;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    public class IdleWander : NPCBehaviourWithAnimation
    {
        private readonly int _exitStatus;
        public IdleWander(int maxFrames, int SurpriseIndex) 
            : base("IdleWander".OmoriModString(), maxFrames)
        {
            _exitStatus = SurpriseIndex;
        }

        protected override void FindFrame(int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            n.frameCounter++;
            if (n.frameCounter % 10 == 0)
            {
                behaviourInfo++;
            }
            n.frame.Y = BehaviourInfo.CurrentFrame * frameHeight;
        }

        protected override void OnStart()
        {
            npc.AI_Timer = 0;
        }

        protected override void AI()
        {
            NPC n = npc.NPC;
            n.TargetClosest(false);
            if (npc.AI_Timer == 0)
            {
                bool leftOrRight = Main.rand.NextBool(2);
                n.direction = leftOrRight ? 1: -1;
                n.netUpdate = true;
            }
            npc.AI_Timer++;

            if (npc.AI_Timer < 120)
            {
                n.velocity.X = n.direction;
                n.velocity.Y = 8f;

                if (n.HasValidTarget && Main.player[n.target].Distance(n.Center) < 500f)
                {
                    npc.AI_Timer = 0;
                    BehaviourInfo.ExitStatus = _exitStatus;
                }
            }
            else
            {
                npc.AI_Timer = 0;
            }
        }
    }
}
