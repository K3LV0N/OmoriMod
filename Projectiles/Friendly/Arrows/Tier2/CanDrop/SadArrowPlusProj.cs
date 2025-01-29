using Terraria;
using OmoriMod.Projectiles.Abstract_Classes.SetSpeedProj;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop
{
    public class SadArrowPlusProj : SetSpeedProj
    {

        public override void SetDefaults()
        {
            SetArrowDefaults();
            SetEmotionType(EmotionType.SAD);
        }

        public override bool PreAI()
        {
            DustTrail();
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            OnKillWithDrop();
        }

        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
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
