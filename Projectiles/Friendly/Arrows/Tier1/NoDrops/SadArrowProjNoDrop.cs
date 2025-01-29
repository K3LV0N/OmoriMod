using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops
{
    public class SadArrowProjNoDrop : SadProj
    {

        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override bool PreAI()
        {
            DustTrail();
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop();
        }
    }
}
