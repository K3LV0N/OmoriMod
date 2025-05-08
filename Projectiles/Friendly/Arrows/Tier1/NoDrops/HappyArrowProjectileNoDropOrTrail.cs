using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops
{
    public class HappyArrowProjectileNoDropOrTrail : HappyProjectile
    {
        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }
    }
}
