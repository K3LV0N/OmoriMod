using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Magic.Tier2;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier4
{
    public class SadBombPlus : SadProj
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<AngryBombPlus>());
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
            AI_MagicBombProjectileWithFlip<BundledSadnessNoTrail>(damagePerProjectile: 32, projectileSpeedOnSpawn: 6, volleys: 6, shotsPerVolley: 4, interval: 30);
        }
    }
}
