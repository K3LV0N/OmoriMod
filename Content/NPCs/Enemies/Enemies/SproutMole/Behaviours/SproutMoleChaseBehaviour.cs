using OmoriMod.Content.NPCs.Enemies.General_Behaviours.Actives.Chase_Player;

namespace OmoriMod.Content.NPCs.Enemies.Enemies.SproutMole.Behaviours
{
    public class SproutMoleChaseBehaviour(int jumpIndex, int exitStatus) 
        : ChasePlayerExitOnTooFarFromTarget(
            jumpIndex: jumpIndex,
            exitStatus: exitStatus,
            speed: 2.5f,
            inertia: 20f,
            maxDistance: 800f
            )
    {
    }
}
