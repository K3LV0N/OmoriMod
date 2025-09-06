using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Content.NPCs.StateManagement.NPCBehaviours;
using OmoriMod.Util;
using System.ComponentModel;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    internal class ChasePlayer : NPCBehaviourWithAnimation
    {
        private readonly int _jumpStatus;
        private readonly int _idleStatus;
        public ChasePlayer(int maxFrames, int jumpIndex, int idleIndex) 
            : base("ChasePlayer".OmoriModString(), maxFrames)
        {
            _jumpStatus = jumpIndex;
            _idleStatus = idleIndex;
        }

        protected override void FindFrame(int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            n.frameCounter++;
            if (n.frameCounter % 5 == 0) {
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
            n.TargetClosest(true);

            if (Main.player[n.target].Distance(n.Center) > 800f)
            {
                BehaviourInfo.ExitStatus = _idleStatus;
            }
            else if (n.collideX)
            {
                BehaviourInfo.ExitStatus = _jumpStatus;
            }
            else
            {
                float speed = 2f;
                float inertia = 25f;

                AIHelper.MoveHorizontal(n, speed, inertia, n.direction);
            }
        }
    }
}
