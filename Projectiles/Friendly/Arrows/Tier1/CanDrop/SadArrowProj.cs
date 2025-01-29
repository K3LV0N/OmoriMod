using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier1.CanDrop
{
    public class SadArrowProj : SadProj
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
    }
}
