using OmoriMod.Content.NPCs.Enemies.General_Behaviours.Actives.Chase_Player;

namespace OmoriMod.Content.NPCs.Enemies.Enemies.SproutMole.Behaviours
{
    public class SproutMoleJumpChaseBehaviour(int exitStatus)
        : ChasePlayerJump(
            chaseIndex: exitStatus,
            speed: 2.5f,
            inertia: 20f
            )
    {
    }
}
