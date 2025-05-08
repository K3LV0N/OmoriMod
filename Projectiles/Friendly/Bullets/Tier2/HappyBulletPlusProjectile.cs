using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;

namespace OmoriMod.Projectiles.Friendly.Bullets.Tier2
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
            AI_MultiSplittingProjectile<HappyBulletProjectileNoTrail>(maxAngle: 32, ProjectileAmount: 9);
        }
    }
}
