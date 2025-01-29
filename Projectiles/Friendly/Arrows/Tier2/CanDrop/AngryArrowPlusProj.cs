using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop
{
    public class AngryArrowPlusProj : AngryProj
    {
        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillWithDrop<AngryArrowPlus>(timeLeft);
        }

        public override bool PreAI()
        {
            DustTrail();
            return true;
        }
        public override void AI()
        {
            AI_AngryHeatSeekingProj();
        }
    }
}
