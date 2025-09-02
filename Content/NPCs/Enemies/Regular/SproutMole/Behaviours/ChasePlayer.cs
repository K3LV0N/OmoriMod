using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.StateManagement;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    internal class ChasePlayer : NPCBehaviour
    {
        private readonly int _jumpStatus;
        private readonly int _idleStatus;
        public ChasePlayer(int jumpIndex, int idleIndex)
        {
            behaviourName = OmoriString.str("ChasePlayer");
            _jumpStatus = jumpIndex;
            _idleStatus = idleIndex;
        }

        protected override void OnStart()
        {
            npc.AI_Timer = 0;
        }
        protected override int AI()
        {
            NPC n = npc.NPC;
            n.TargetClosest(true);

            if (Main.player[n.target].Distance(n.Center) > 800f)
            {
                return _idleStatus;
            }
            else if (n.collideX)
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
