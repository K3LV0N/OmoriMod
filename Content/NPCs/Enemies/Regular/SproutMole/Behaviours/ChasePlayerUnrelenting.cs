using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Content.NPCs.StateManagement.NPCBehaviours;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    internal class ChasePlayerUnrelenting : NPCBehaviourWithAnimation
    {
        private readonly int _jumpStatus;
        public ChasePlayerUnrelenting(int maxFrames, int jumpIndex) 
            : base("ChasePlayerUnrelenting".OmoriModString(), maxFrames)
        {
            _jumpStatus = jumpIndex;
        }

        protected override void FindFrame(int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            n.frameCounter++;
            if (n.frameCounter % 5 == 0)
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
            n.TargetClosest(true);

            if (n.collideX)
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
