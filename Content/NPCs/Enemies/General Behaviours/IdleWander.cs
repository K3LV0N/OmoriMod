using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours
{
    public class IdleWander(int SurpriseIndex) : NPCBehaviour()
    {
        private readonly int _exitStatus = SurpriseIndex;

        protected override void FindFrame(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo, int frameHeight)
        {
            NPC n = npc.NPC;
            n.spriteDirection = n.direction;

            n.frameCounter++;
            if (n.frameCounter % 10 == 0)
            {
                behaviourInfo++;
            }
            n.frame.Y = behaviourInfo.CurrentFrame * frameHeight;
        }

        protected override void OnStart(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            npc.AI_Timer = 0;
        }

        protected override void AI(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            NPC n = npc.NPC;
            n.TargetClosest(false);
            if (npc.AI_Timer == 0)
            {
                bool leftOrRight = Main.rand.NextBool(2);
                n.direction = leftOrRight ? 1: -1;
                n.netUpdate = true;
            }
            
            n.velocity.X = n.direction;

            if (n.HasValidTarget && Main.player[n.target].Distance(n.Center) < 500f)
            {
                npc.AI_Timer = 0;
                behaviourInfo.ExitStatus = _exitStatus;
            }

            if (npc.AI_Timer >= 120)
            {
                npc.AI_Timer = 0;
            }
            npc.AI_Timer++;
        }
    }
}
