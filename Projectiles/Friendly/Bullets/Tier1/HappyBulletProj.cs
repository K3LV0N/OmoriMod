using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Projectiles.Abstract_Classes;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Bullets.Tier1
{
    public class HappyBulletProj : HappyProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;

            Projectile.aiStyle = 0;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 12;

            Projectile.arrow = false;
        }

        public override bool PreAI()
        {
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Yellow);
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
