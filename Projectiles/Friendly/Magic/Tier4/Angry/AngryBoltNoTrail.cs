﻿using OmoriMod.Projectiles.Abstract_Classes;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier4.Angry
{
    public class AngryBoltNoTrail : AngryProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = 1;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
