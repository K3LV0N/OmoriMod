using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Bullets.Tier2
{
    public class SadBulletPlusProj : SadProj
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
            float speedDesired = 20f;
            float gravRequested = 0f;
            bool gravity = false;
            AI_SetSpeedProj(speedDesired, gravRequested, gravity);
        }
    }
}
