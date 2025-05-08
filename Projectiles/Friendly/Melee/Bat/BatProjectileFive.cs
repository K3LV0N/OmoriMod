using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Melee.Bat
{
    public class BatProjectileFive : AngryProjectile
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
            AI_SplittingProjectile<BatProjectile>(maxAngle: 20, ProjectileAmount: 5);
        }
    }
}
