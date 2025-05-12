﻿using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Magic.Tier1;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier2
{
    public class AngryBundleProjectileNoDust : AngryProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<AngryBundleProjectile>());
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft, noSound: true);
        }

        public override void AI()
        {
            AI_TravelingBundleProjectile<AngryBoltProjectileNoDust>(damagePerProjectile: 18, ProjectileSpeedOnSpawn: 6, volleys: 4, interval: 60);
        }
    }
}
