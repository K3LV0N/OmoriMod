using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    public class IdleWander : NPCBehaviour
    {
        private readonly int _exitStatus;
        public IdleWander(int SurpriseIndex) {
            behaviourName = OmoriString.str("IdleWander");
            _exitStatus = SurpriseIndex;
        }

        protected override void OnStart()
        {
            npc.AI_Timer = 0;
        }

        protected override int AI()
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
                    return _exitStatus;
                }
            }
            else
            {
                npc.AI_Timer = 0;
            }

            return -1;
        }
    }
}
