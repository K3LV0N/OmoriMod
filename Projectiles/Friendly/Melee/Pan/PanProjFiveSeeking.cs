using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Melee.Bat;

namespace OmoriMod.Projectiles.Friendly.Melee.Pan
{
    public class PanProjFiveSeeking : HappyProj
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<BatProj>());
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
