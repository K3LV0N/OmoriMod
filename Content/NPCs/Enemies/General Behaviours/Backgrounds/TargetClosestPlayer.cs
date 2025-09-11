using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using OmoriMod.Content.NPCs.State_Management.NPC_Behaviour;
using OmoriMod.Util;
using Terraria;


namespace OmoriMod.Content.NPCs.Enemies.General_Behaviours.Backgrounds
{
    /// <summary>
    /// Targets the closest player every <paramref name="timer"/> seconds.
    /// </summary>
    /// <param name="timer">How long between <see cref="NPC.TargetClosest(bool)"/> calls. If <c>null</c>, then this gets called every tick<see cref=""/></param>
    /// <param name="faceTarget">Whether the npc should face its target.</param>
    public class TargetClosestPlayer(TickTimer timer = null, bool faceTarget=false) : NPCBackgroundBehaviour()
    {
        TickTimer Timer = timer;

        protected override void OnStart(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            npc.NPC.TargetClosest(faceTarget);
        }
        protected override void AI(OmoriBehaviourNPC npc, BehaviourInfo behaviourInfo)
        {
            if(Timer is null)
            {
                npc.NPC.TargetClosest(faceTarget);
            }
            else
            {
                if (Timer.IsDone)
                {
                    npc.NPC.TargetClosest(faceTarget);
                    Timer.Reset();
                }
                else
                {
                    Timer--;
                }
            }
        }
    }
}
