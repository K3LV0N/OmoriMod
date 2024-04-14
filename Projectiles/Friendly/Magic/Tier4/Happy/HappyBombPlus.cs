using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Projectiles.Abstract_Classes;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier4.Happy
{
    public class HappyBombPlus : HappyProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = 5;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = 0;
        }

        public override bool PreAI()
        {
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Yellow);
            return base.PreAI();
        }
        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public override void AI()
        {
            AI_Timer++;

            if (AI_Timer % 60 == 0)
            {
                Vector2 proj2 = new Vector2(1, -1);
                Vector2 proj4 = new Vector2(1, 1);
                Vector2 proj6 = new Vector2(-1, 1);
                Vector2 proj8 = new Vector2(-1, -1);
                proj2.Normalize();
                proj4.Normalize();
                proj6.Normalize();
                proj8.Normalize();
                proj2 = proj2 * 6;
                proj4 = proj4 * 6;
                proj6 = proj6 * 6;
                proj8 = proj8 * 6;
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                        ModContent.ProjectileType<BundledHappinessNoTrail>(), 32, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                        ModContent.ProjectileType<BundledHappinessNoTrail>(), 32, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj6,
                        ModContent.ProjectileType<BundledHappinessNoTrail>(), 32, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj8,
                        ModContent.ProjectileType<BundledHappinessNoTrail>(), 32, Projectile.knockBack, Projectile.owner);
            }
            else if (AI_Timer % 30 == 0)
            {
                Vector2 proj1 = new Vector2(0, -1);
                Vector2 proj3 = new Vector2(1, 0);
                Vector2 proj5 = new Vector2(0, 1);
                Vector2 proj7 = new Vector2(-1, 0);
                proj1.Normalize();
                proj3.Normalize();
                proj5.Normalize();
                proj7.Normalize();
                proj1 = proj1 * 6;
                proj3 = proj3 * 6;
                proj5 = proj5 * 6;
                proj7 = proj7 * 6;

                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj1,
                        ModContent.ProjectileType<BundledHappinessNoTrail>(), 32, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                        ModContent.ProjectileType<BundledHappinessNoTrail>(), 32, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                        ModContent.ProjectileType<BundledHappinessNoTrail>(), 32, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj7,
                        ModContent.ProjectileType<BundledHappinessNoTrail>(), 32, Projectile.knockBack, Projectile.owner);

            }

            if (AI_Timer == 180)
            {
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
