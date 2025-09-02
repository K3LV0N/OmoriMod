using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    public class ChasePlayerJump : NPCBehaviour
    {
        private readonly int _exitStatus;
        public ChasePlayerJump(int chaseIndex)
        {
            behaviourName = OmoriString.str("ChasePlayerJump");
            _exitStatus = chaseIndex;
        }

        protected override void OnStart()
        {
            npc.AI_Timer = 0;
        }

        protected override int AI()
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
                return _exitStatus;
            }
            npc.AI_Timer++;
            return -1;
        }
    }
}
