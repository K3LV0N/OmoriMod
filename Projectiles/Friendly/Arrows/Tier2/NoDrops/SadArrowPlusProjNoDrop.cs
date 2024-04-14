using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes.SetSpeedProj;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops
{
    public class SadArrowPlusProjNoDrop : SetSpeedProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;
            Projectile.penetrate = 100;

            Projectile.aiStyle = -1;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 50;

            Projectile.arrow = true;

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
            float speedDesired = 17f;
            float gravRequested = 0.11f;
            bool gravity = true;
            AI_SetSpeedProj(speedDesired, gravRequested, gravity);
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            AI_Timer = 0;
        }
    }
}
