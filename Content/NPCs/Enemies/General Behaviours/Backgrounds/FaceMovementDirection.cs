﻿using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using OmoriMod.Content.NPCs.State_Management.NPC_Behaviour;
using System;
using Terraria;

namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours.Backgrounds
{
    /// <summary>
    /// Makes the NPC face the direction they are moving.
    /// </summary>
    public class FaceMovementDirection() : NPCBackgroundBehaviour()
    {
        protected override void AI(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            NPC n = npc.NPC;
            if (float.IsNaN(n.velocity.X)) { return; }
            int direction = Math.Sign(n.velocity.X);
            if (direction != 0) { n.direction = direction; }
        }
    }
}
