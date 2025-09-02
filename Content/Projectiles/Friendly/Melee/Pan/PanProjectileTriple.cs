﻿using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Content.Projectiles.Friendly.Melee.Bat;

namespace OmoriMod.Content.Projectiles.Friendly.Melee.Pan
{
    public class PanProjectileTriple : HappyProjectile
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
            AI_SplittingProjectile<PanProjectile>(maxAngle: 10, ProjectileAmount: 3);
        }
    }
}
