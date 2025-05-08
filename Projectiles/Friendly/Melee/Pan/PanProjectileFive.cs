using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Melee.Bat;

namespace OmoriMod.Projectiles.Friendly.Melee.Pan
{
    public class PanProjectileFive : HappyProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<BatProjectile>());
        }

        public override void OnKill(int timeleft)
        {
            OnKillNoDrop(timeleft, noSound: true);
            MakeDust();
        }

        public override void AI()
        {
            AI_SplittingProjectile<PanProjectile>(maxAngle: 20, ProjectileAmount: 5);
        }
    }
}
