using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Bullets.Tier1
{
    public class AngryBulletProjectile : AngryProjectile
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
    }
}
