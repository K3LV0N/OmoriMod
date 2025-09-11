using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using OmoriMod.Content.NPCs.State_Management.NPC_Behaviour;

namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours.Tests
{
    /// <summary>
    /// Fucking launches the NPC into orbit. For testing
    /// </summary>
    public class ShootToTheMoonAhhBehaviour() : NPCBehaviour(0)
    {
        
        protected override void AI(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            npc.NPC.velocity.Y = -80f;
        }
    }
}
