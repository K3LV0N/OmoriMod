using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Content.NPCs.StateManagement.NPCBehaviours;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    public class SuprisedJump : NPCBehaviourWithAnimation
    {
        private readonly int _exitStatus;
        public SuprisedJump(int maxFrames,  int chaseIndex) 
            : base("SuprisedJump".OmoriModString(), maxFrames)
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
            n.TargetClosest(true);

            if (npc.AI_Timer == 0)
            {
                n.velocity.Y = -3f;
            }

            if (n.velocity.Y <= 0)
            {
                n.velocity.Y += 0.05f;
            }

            if (n.collideY)
            {
                BehaviourInfo.ExitStatus = _exitStatus;
            }

            npc.AI_Timer++;
        }
    }
}
