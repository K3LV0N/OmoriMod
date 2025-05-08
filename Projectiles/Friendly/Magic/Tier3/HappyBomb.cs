using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier1;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier3
{
    public class HappyBomb : HappyProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<AngryBomb>());
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }

        public override bool PreAI()
        {
            MakeDust();
            return true;
        }

        public override void AI()
        {
            AI_MagicBombProjectile<HappyBolt>(damagePerProjectile: 18, ProjectileSpeedOnSpawn: 6, volleys: 6, shotsPerVolley: 8, interval: 30);
        }
    }
}
