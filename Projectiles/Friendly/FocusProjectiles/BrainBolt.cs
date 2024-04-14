using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OmoriMod.Projectiles.Friendly.FocusProjectiles
{
    public class BrainBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            // size
            Projectile.height = 4;
            Projectile.width = 4;

            // tile interation
            Projectile.tileCollide = true;

            // ai
            Projectile.aiStyle = 0; // 0 is bullet

            // drawing offset
            DrawOffsetX = -7;
            DrawOriginOffsetX = 1;
            DrawOriginOffsetY = -14;

            // friendly
            Projectile.friendly = true;
        }

        public override void PostAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2 + .15f;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        }
    }
}
