using Terraria.ModLoader;
using OmoriMod.Content.Projectiles.Abstract_Classes;

namespace OmoriMod.Content.Projectiles.Friendly.Melee.Bat
{
    public class BatProjectileTriple : AngryProjectile
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
            AI_SplittingProjectile<BatProjectile>(maxAngle: 10, ProjectileAmount: 3);
        }
    }
}
