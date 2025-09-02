using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    internal class ChasePlayerUnrelenting : NPCBehaviour
    {
        private readonly int _jumpStatus;
        public ChasePlayerUnrelenting(int jumpIndex)
        {
            behaviourName = OmoriString.str("ChasePlayerUnrelenting");
            _jumpStatus = jumpIndex;
        }

        protected override void OnStart()
        {
            npc.AI_Timer = 0;
        }
        protected override int AI()
        {
            NPC n = npc.NPC;
            n.TargetClosest(true);

            if (n.collideX)
            {
                return _jumpStatus;
            }
            else
            {
                float speed = 2f;
                float inertia = 25f;

                AIHelper.MoveHorizontal(n, speed, inertia, n.direction);
                return -1;
            }
        }
    }
}
