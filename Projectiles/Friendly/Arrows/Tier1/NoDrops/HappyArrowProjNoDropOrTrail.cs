using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops
{
    public class HappyArrowProjNoDropOrTrail : HappyProj
    {

        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop();
        }
    }
}
