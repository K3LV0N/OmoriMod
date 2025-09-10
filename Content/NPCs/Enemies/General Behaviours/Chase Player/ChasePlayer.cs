using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours.Chase_Player
{
    public class ChasePlayer(int jumpIndex, float speed, float inertia, int exitStatus = -1) : NPCBehaviour()
    {
        private readonly int _jumpStatus = jumpIndex;
        private readonly int _exitStatus = exitStatus;
        private readonly float _speed = speed;
        private readonly float _inertia = inertia;
        protected override void FindFrame(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo, int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            n.frameCounter++;
            if (n.frameCounter % 5 == 0)
            {
                behaviourInfo++;
            }
            n.frame.Y = behaviourInfo.CurrentFrame * frameHeight;
        }


        protected override void OnStart(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            npc.AI_Timer = 0;
        }

        /// <summary>
        /// Override to make this exit with a condition
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="behaviourInfo"></param>
        /// <returns></returns>
        protected virtual bool ExitCondition(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo) { return false; }

        protected override void AI(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            NPC n = npc.NPC;
            n.TargetClosest(true);

            npc.MoveHorizontal(_speed, _inertia, n.direction);
            npc.AI_Timer++;

            if (ExitCondition(npc, behaviourInfo))
            {
                behaviourInfo.ExitStatus = _exitStatus;
                return;
            }
            if (n.collideX)
            {
                behaviourInfo.ExitStatus = _jumpStatus;
                return;
            }
        }
    }
}
