using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops
{
    public class HappyArrowPlusProjNoDrop : HappyProj
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
            AI_MultiSplittingProj<HappyArrowProjNoDropOrTrail>(maxAngle: 32, projectileAmount: 9);
        }
    }
}
