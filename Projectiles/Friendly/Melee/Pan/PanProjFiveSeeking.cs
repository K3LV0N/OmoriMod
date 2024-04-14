using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Projectiles.Abstract_Classes;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Melee.Pan
{
    public class PanProjFiveSeeking : HappyProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = 0;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.alpha = 50;

            Projectile.arrow = false;
        }

        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI()
        {
            if (AI_Timer == 0)
            {
                Quaternion proj1Q = new Quaternion(0, 0, 1, 0.170f);
                Quaternion proj2Q = new Quaternion(0, 0, 1, -0.170f);
                Quaternion proj3Q = new Quaternion(0, 0, 1, 0.085f);
                Quaternion proj4Q = new Quaternion(0, 0, 1, -0.085f);
                Vector2 proj1 = Vector2.Transform(Projectile.velocity, proj1Q);
                Vector2 proj2 = Vector2.Transform(Projectile.velocity, proj2Q);
                Vector2 proj3 = Vector2.Transform(Projectile.velocity, proj3Q);
                Vector2 proj4 = Vector2.Transform(Projectile.velocity, proj4Q);
                proj1 = Vector2.Negate(proj1);
                proj2 = Vector2.Negate(proj2);
                proj3 = Vector2.Negate(proj3);
                proj4 = Vector2.Negate(proj4);


                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj1,
                        ModContent.ProjectileType<PanProjSeeking>(), 90, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                        ModContent.ProjectileType<PanProjSeeking>(), 90, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                        ModContent.ProjectileType<PanProjSeeking>(), 90, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                        ModContent.ProjectileType<PanProjSeeking>(), 90, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity,
                        ModContent.ProjectileType<PanProjSeeking>(), 90, Projectile.knockBack, Projectile.owner);
                Projectile.Kill();
            }
            AI_Timer++;


        }

        public override void OnKill(int timeleft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Yellow);
        }

    }
}
