using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop
{
    public class HappyArrowPlusProj : HappyProj
    {
        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillWithDrop<HappyArrowPlus>(timeLeft);
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
