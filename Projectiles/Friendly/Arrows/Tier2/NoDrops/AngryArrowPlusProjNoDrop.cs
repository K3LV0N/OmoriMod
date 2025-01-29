using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops
{
    public class AngryArrowPlusProjNoDrop : AngryProj
    {
        public override void SetDefaults()
        {
            SetArrowDefaults();
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
            AI_AngryHeatSeekingProj();
        }
    }
}
