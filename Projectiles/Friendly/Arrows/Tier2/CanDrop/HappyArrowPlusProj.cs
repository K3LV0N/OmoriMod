using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop
{
    public class HappyArrowPlusProj : HappyProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;

            Projectile.aiStyle = ProjAIStyleID.Arrow;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 24;

            Projectile.arrow = true;
        }

        public override bool PreAI()
        {
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Yellow);
            return true;
        }


        public float AI_Timer
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
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
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj6,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj7,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj8,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
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
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj6,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj7,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj8,
                    ModContent.ProjectileType<HappyArrowProjNoDropOrTrail>(), 10, Projectile.knockBack, Projectile.owner);
            }


        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            if (Projectile.owner == Main.myPlayer)
            {
                //has a chance to drop arrow for pickup
                int item = Main.rand.NextBool(5) ? Item.NewItem(Entity.GetSource_Death(), Projectile.getRect(), ModContent.ItemType<HappyArrowPlus>()) : 0;
            }
        }
    }
}
