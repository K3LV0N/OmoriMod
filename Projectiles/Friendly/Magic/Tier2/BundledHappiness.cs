using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier1;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier2
{
    public class BundledHappiness : HappyProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<BundledAnger>());
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft, noSound: true);
        }

        public override void AI()
        {
            AI_BundledProjectile<HappyBolt>(damagePerProjectile: 18, ProjectileSpeedOnSpawn: 6, volleys: 4, shotsPerVolley: 5, interval: 60);
        }
    }
}
