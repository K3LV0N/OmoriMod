using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Content.NPCs.StateManagement.NPCBehaviours;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    public class ChasePlayerJump : NPCBehaviourWithAnimation
    {
        private readonly int _exitStatus;
        public ChasePlayerJump(int maxFrames, int chaseIndex)
            : base("ChasePlayerJump".OmoriModString(), maxFrames)
        {
            _exitStatus = chaseIndex;
        }

        protected override void FindFrame(int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            behaviourInfo.CurrentFrame = 2;
            n.frame.Y = BehaviourInfo.CurrentFrame * frameHeight;
        }

        protected override void OnStart()
        {
            npc.AI_Timer = 0;
        }

        protected override void AI()
        {
            NPC n = npc.NPC;
            if (npc.AI_Timer == 0) {
                n.velocity.Y += -10f;
            }
            
            float speed = 2f;
            float inertia = 25f;

            AIHelper.MoveHorizontal(n, speed, inertia, n.direction);

            if (npc.AI_Timer > 3 && n.collideY)
            {
                BehaviourInfo.ExitStatus = _exitStatus;
            }
            npc.AI_Timer++;
        }
    }
}
