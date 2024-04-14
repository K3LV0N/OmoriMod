using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace OmoriMod.Projectiles.Friendly.BossRelated.YeOldSprout
{
    public class SproutBulletProj : ModProjectile
    {
        public override void SetDefaults()
        {
            // size
            Projectile.width = 8;
            Projectile.height = 8;

            // friendly
            Projectile.friendly = true;

            // bullet AI
            Projectile.aiStyle = 0;

            // not an arrow
            Projectile.arrow = false;
        }

        public override bool PreAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
