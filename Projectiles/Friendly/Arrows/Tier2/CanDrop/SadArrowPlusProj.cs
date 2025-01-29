using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop
{
    public class SadArrowPlusProj : SadProj
    {
        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillWithDrop(timeLeft);
        }

        public override bool PreAI()
        {
            DustTrail();
            return true;
        }

        public override void AI()
        {
            float speedDesired = 17f;
            float gravRequested = 0.11f;
            bool gravity = true;
            AI_SetSpeedProj(speedDesired, gravRequested, gravity);
        }
    }
}
