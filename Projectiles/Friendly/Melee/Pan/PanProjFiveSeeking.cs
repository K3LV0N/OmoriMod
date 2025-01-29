using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Melee.Pan
{
    public class PanProjFiveSeeking : HappyProj
    {
        public override void SetDefaults()
        {
            SetOtherDefaults(width: 32, height: 32, damageType: DamageClass.Melee, aiStyle: 0, penetration: 1, scale: 1, tileCollide: true, timeLeft: 300, alpha: 50);
        }

        public override void OnKill(int timeleft)
        {
            OnKillNoDrop(timeleft, noSound: true);
            DustTrail();
        }

        public override void AI()
        {
            AI_SplittingProj<PanProjSeeking>(maxAngle: 20, projectileAmount: 5);
        }
    }
}
