using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    public class SuprisedJump : NPCBehaviour
    {
        private readonly int _exitStatus;
        public SuprisedJump(int chaseIndex)
        {
            behaviourName = OmoriString.str("SuprisedJump");
            _exitStatus = chaseIndex;
        }

        protected override void OnStart()
        {
            npc.AI_Timer = 0;
        }
        protected override int AI()
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
                return _exitStatus;
            }

            npc.AI_Timer++;
            return -1;
        }
    }
}
