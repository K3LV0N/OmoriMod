﻿using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using OmoriMod.Content.NPCs.State_Management.NPC_Behaviour;
using OmoriMod.Util;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours.Actives.Chase_Player
{
    public class ChasePlayerExitOnTimeOut(int exitStatus, float speed, float inertia, TickTimer timeOut) : NPCBehaviour(exitStatus)
    {
        private TickTimer _timeOut = timeOut;
        

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
            subBehaviour = new ChasePlayerJump(1, speed, inertia);
            npc.AI_Timer = 0;
            _timeOut.Reset();
        }

        protected override void GuaranteedAI(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            // transition to next behaviour if the timer is done and the npc is on the ground
            _timeOut--;
            if (_timeOut.IsDone)
            {
                if(npc.NPC.collideY)
                {
                    behaviourInfo.ExitStatus = _defaultExitStatus;
                    return;
                }
            }
        }

        protected override void AI(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            NPC n = npc.NPC;

            npc.MoveHorizontal(speed, inertia, npc.DirectionToTarget());
            npc.AI_Timer++;

            if (n.collideX)
            {
                ReadySubBehaviour(behaviourInfo);
                return;
            }
        }
    }
}
