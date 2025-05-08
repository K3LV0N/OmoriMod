using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Bullets.Tier1
{
    public class HappyBulletProjectileNoTrail : HappyProjectile
    {
        public override void SetDefaults()
        {
            SetBulletDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }
    }
}
