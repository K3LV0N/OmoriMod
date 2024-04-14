using Microsoft.Xna.Framework;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Tests
{
    internal class TestProj : HappyProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;

            Projectile.aiStyle = 0;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.arrow = false;
        }

        public override bool PreAI()
        {
            Dust.NewDust(Projectile.position, 2, 2, DustID.Pixie, 0f, 0f, 0, Color.Yellow);
            return true;
        }

        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI()
        {
            AI_Timer++;

            if (AI_Timer == 5)
            {
                Quaternion proj1Q = new Quaternion(0, 0, 1, 0.120f);
                Quaternion proj2Q = new Quaternion(0, 0, 1, -0.120f);
                Quaternion proj3Q = new Quaternion(0, 0, 1, 0.085f);
                Quaternion proj4Q = new Quaternion(0, 0, 1, -0.085f);
                Quaternion proj5Q = new Quaternion(0, 0, 1, 0.050f);
                Quaternion proj6Q = new Quaternion(0, 0, 1, -0.050f);
                Quaternion proj7Q = new Quaternion(0, 0, 1, 0.015f);
                Quaternion proj8Q = new Quaternion(0, 0, 1, -0.015f);
                Vector2 proj1 = Vector2.Transform(Projectile.velocity, proj1Q);
                Vector2 proj2 = Vector2.Transform(Projectile.velocity, proj2Q);
                Vector2 proj3 = Vector2.Transform(Projectile.velocity, proj3Q);
                Vector2 proj4 = Vector2.Transform(Projectile.velocity, proj4Q);
                Vector2 proj5 = Vector2.Transform(Projectile.velocity, proj5Q);
                Vector2 proj6 = Vector2.Transform(Projectile.velocity, proj6Q);
                Vector2 proj7 = Vector2.Transform(Projectile.velocity, proj7Q);
                Vector2 proj8 = Vector2.Transform(Projectile.velocity, proj8Q);
                proj1 = Vector2.Negate(proj1);
                proj2 = Vector2.Negate(proj2);
                proj3 = Vector2.Negate(proj3);
                proj4 = Vector2.Negate(proj4);
                proj5 = Vector2.Negate(proj5);
                proj6 = Vector2.Negate(proj6);
                proj7 = Vector2.Negate(proj7);
                proj8 = Vector2.Negate(proj8);

                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj1,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj6,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj7,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj8,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
            }

            if (AI_Timer == 20)
            {
                Quaternion proj1Q = new Quaternion(0, 0, 1, 0.120f);
                Quaternion proj2Q = new Quaternion(0, 0, 1, -0.120f);
                Quaternion proj3Q = new Quaternion(0, 0, 1, 0.085f);
                Quaternion proj4Q = new Quaternion(0, 0, 1, -0.085f);
                Quaternion proj5Q = new Quaternion(0, 0, 1, 0.050f);
                Quaternion proj6Q = new Quaternion(0, 0, 1, -0.050f);
                Quaternion proj7Q = new Quaternion(0, 0, 1, 0.015f);
                Quaternion proj8Q = new Quaternion(0, 0, 1, -0.015f);
                Vector2 proj1 = Vector2.Transform(Projectile.velocity, proj1Q);
                Vector2 proj2 = Vector2.Transform(Projectile.velocity, proj2Q);
                Vector2 proj3 = Vector2.Transform(Projectile.velocity, proj3Q);
                Vector2 proj4 = Vector2.Transform(Projectile.velocity, proj4Q);
                Vector2 proj5 = Vector2.Transform(Projectile.velocity, proj5Q);
                Vector2 proj6 = Vector2.Transform(Projectile.velocity, proj6Q);
                Vector2 proj7 = Vector2.Transform(Projectile.velocity, proj7Q);
                Vector2 proj8 = Vector2.Transform(Projectile.velocity, proj8Q);
                proj1 = Vector2.Negate(proj1);
                proj2 = Vector2.Negate(proj2);
                proj3 = Vector2.Negate(proj3);
                proj4 = Vector2.Negate(proj4);
                proj5 = Vector2.Negate(proj5);
                proj6 = Vector2.Negate(proj6);
                proj7 = Vector2.Negate(proj7);
                proj8 = Vector2.Negate(proj8);

                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj1,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj6,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj7,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj8,
                    ModContent.ProjectileType<HappyBulletProjNoTrail>(), 4, Projectile.knockBack, Projectile.owner);
            }


        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        }
    }
}
