using OmoriMod.Content.NPCs.Enemies.General_Behaviours.Chase_Player;

namespace OmoriMod.Content.NPCs.Enemies.Regular.SproutMole.Behaviours
{
    public class SproutMoleChaseBehaviour(int jumpIndex, int exitStatus) 
        : ChasePlayerExitOnTooFarFromTarget(
            jumpIndex: jumpIndex,
            exitStatus: exitStatus,
            speed: 1.5f,
            inertia: 25f,
            maxDistance: 800f
            )
    {
    }
}
