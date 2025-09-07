using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours
{
    public class SuprisedJump(int chaseIndex) : NPCBehaviour()
    {
        private readonly int _exitStatus = chaseIndex;

        protected override void FindFrame(OmoriModNPC npc, BehaviourInfo behaviourInfo, int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            behaviourInfo.CurrentFrame = 2;
            n.frame.Y = behaviourInfo.CurrentFrame * frameHeight;
        }

        protected override void OnStart(OmoriModNPC npc, BehaviourInfo behaviourInfo)
        {
            npc.AI_Timer = 0;
        }
        protected override void AI(OmoriModNPC npc, BehaviourInfo behaviourInfo)
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
                behaviourInfo.ExitStatus = _exitStatus;
            }

            npc.AI_Timer++;
        }
    }
}
