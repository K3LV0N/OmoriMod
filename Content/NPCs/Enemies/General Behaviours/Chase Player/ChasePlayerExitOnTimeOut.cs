using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours.Chase_Player
{
    public class ChasePlayerExitOnTimeOut(int jumpIndex, int exitStatus, float speed, float inertia, TickTimer timeOut) : ChasePlayer(jumpIndex, speed, inertia, exitStatus)
    {
        private readonly TickTimer _timeOut = timeOut;

        protected override void FindFrame(OmoriModNPC npc, BehaviourInfo behaviourInfo, int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            n.frameCounter++;
            if (n.frameCounter % 5 == 0) {
                behaviourInfo++;
            }
            n.frame.Y = behaviourInfo.CurrentFrame * frameHeight;
        }

        protected override void OnStart(OmoriModNPC npc, BehaviourInfo behaviourInfo)
        {
            npc.AI_Timer = 0;
        }

        protected override bool ExitCondition(OmoriModNPC npc, BehaviourInfo behaviourInfo)
        {
            return npc.AI_Timer > _timeOut.TotalTicks;
        }
    }
}
