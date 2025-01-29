using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Bullets.Tier1
{
    public class HappyBulletProj : HappyProj
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
    }
}
