using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;

namespace OmoriMod.Projectiles.Friendly.Bullets.Tier2
{
    public class HappyBulletPlusProj : HappyProj
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
            DustTrail();
            return true;
        }

        public override void AI()
        {
            AI_MultiSplittingProj<HappyBulletProjNoTrail>(maxAngle: 32, projectileAmount: 9);
        }
    }
}
