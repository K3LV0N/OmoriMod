﻿using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Magic.Tier2;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier4
{
    public class AngryBombPlus : AngryProjectile
    {
        public override void SetDefaults()
        {
            SetOtherDefaults(width: 24, height: 24, damageType: DamageClass.Magic, aiStyle: 0, penetration: 5, scale: 1, tileCollide: true);
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
            AI_MagicBombProjectileWithFlip<BundledAngerNoTrail>(damagePerProjectile: 32, ProjectileSpeedOnSpawn: 6, volleys: 6, shotsPerVolley: 4, interval: 30);
        }
    }
}
