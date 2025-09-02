﻿using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Content.Projectiles.Friendly.Melee.Bat;

namespace OmoriMod.Content.Projectiles.Friendly.Melee.Knife
{
    public class KnifeProjectileTriple : SadProjectile
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
            AI_SplittingProjectile<KnifeProjectile>(maxAngle: 10, ProjectileAmount: 3);
        }
    }
}
