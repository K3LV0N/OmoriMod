using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.BossRelated.YeOldSprout
{
    public class SproutBulletProjectile : OmoriModProjectile
    {
        public override void SetDefaults()
        {
            // Set bullet Projectile defaults
            SetBulletDefaults(width: 8, height: 8);
        }

        public override bool PreAI()
        {   
            VelocityRotate(flip: true);
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }
    }
}
