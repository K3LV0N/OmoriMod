using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes.SetSpeedProj;

namespace OmoriMod.Projectiles.Friendly.Bullets.Tier2
{
    public class SadBulletPlusProj : SetSpeedProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.penetrate = 50;

            Projectile.aiStyle = 0;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 30;

            Projectile.arrow = false;


            emotion = emotionType.SAD;
        }

        public override bool PreAI()
        {
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Blue);
            return true;
        }

        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI()
        {
            float speedDesired = 20f;
            float gravRequested = 0f;
            bool gravity = false;
            AI_SetSpeedProj(speedDesired, gravRequested, gravity);
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
