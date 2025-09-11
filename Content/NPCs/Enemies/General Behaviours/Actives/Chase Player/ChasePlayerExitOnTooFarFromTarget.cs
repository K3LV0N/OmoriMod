using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours.Actives.Chase_Player
{
    public class ChasePlayerExitOnTooFarFromTarget(int jumpIndex, int exitStatus, float speed, float inertia, float maxDistance) : ChasePlayer(jumpIndex, speed, inertia, exitStatus)
    {
        private readonly float _maxDistance = maxDistance;

        protected override void FindFrame(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo, int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            n.frameCounter++;
            if (n.frameCounter % 5 == 0) {
                behaviourInfo++;
            }
            n.frame.Y = behaviourInfo.CurrentFrame * frameHeight;
        }

        protected override void OnStart(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            npc.AI_Timer = 0;
        }

        protected override bool ExitCondition(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            return Main.player[npc.NPC.target].Distance(npc.NPC.Center) > _maxDistance;
        }
    }
}
