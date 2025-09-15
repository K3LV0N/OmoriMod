using OmoriMod.Content.Projectiles.Friendly.Bullets.Tier1;
using OmoriMod.Content.Projectiles.Abstract_Classes;

namespace OmoriMod.Content.Projectiles.Friendly.Bullets.Tier2
{
    public class HappyBulletPlusProjectile : HappyProjectile
    {
        public override void SetDefaults()
        {
            SetBulletDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }

        public override bool PreAI()
        {
            MakeDust();
            return true;
        }

        public override void AI()
        {
            AI_MultiSplittingProjectile<HappyBulletProjectileNoDust>(maxAngle: 32, ProjectileAmount: 9);
        }
    }
}
