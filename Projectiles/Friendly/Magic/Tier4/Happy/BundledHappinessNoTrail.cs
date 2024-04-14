using System;
using Microsoft.Xna.Framework;
using OmoriMod.Projectiles.Abstract_Classes;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier4.Happy
{
    public class BundledHappinessNoTrail : HappyProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 42;
            Projectile.damage = 0;
            Projectile.penetrate = int.MaxValue;

            Projectile.aiStyle = -1;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;

        }


        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public bool AI_TimerStartX = false;
        public bool AI_TimerStartY = false;
        public override void AI()
        {
            if (Math.Abs(Projectile.velocity.X) > .5)
            {
                Projectile.velocity = new Vector2(Projectile.velocity.X * .97f, Projectile.velocity.Y);
            }
            else
            {
                Projectile.velocity = new Vector2(0, Projectile.velocity.Y);
                AI_TimerStartX = true;
            }

            if (Math.Abs(Projectile.velocity.Y) > .5)
            {
                Projectile.velocity = new Vector2(Projectile.velocity.X, Projectile.velocity.Y * .97f);
            }
            else
            {
                Projectile.velocity = new Vector2(Projectile.velocity.X, 0);
                AI_TimerStartY = true;
            }

            if (AI_TimerStartX && AI_TimerStartY)
            {
                AI_Timer++;

                if (AI_Timer % 5 == 0)
                {
                    Projectile.Center = new Vector2(Projectile.Center.X + 1, Projectile.Center.Y);
                }
                else if (AI_Timer % 5 == 1)
                {
                    Projectile.Center = new Vector2(Projectile.Center.X, Projectile.Center.Y + 1);
                }
                else if (AI_Timer % 5 == 2)
                {
                    Projectile.Center = new Vector2(Projectile.Center.X - 1, Projectile.Center.Y + 1);
                }
                else if (AI_Timer % 5 == 3)
                {
                    Projectile.Center = new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1);
                }
                else if (AI_Timer % 5 == 4)
                {
                    Projectile.Center = new Vector2(Projectile.Center.X + 1, Projectile.Center.Y - 1);
                }


                Projectile.rotation += AI_Timer % 20 * .1f;


                if (AI_Timer == 60)
                {
                    Vector2 proj1 = new Vector2(0, -2);
                    Vector2 proj2 = new Vector2(3, -2);
                    Vector2 proj3 = new Vector2(-3, -2);
                    Vector2 proj4 = new Vector2(2, 3);
                    Vector2 proj5 = new Vector2(-2, 3);
                    proj1.Normalize();
                    proj2.Normalize();
                    proj3.Normalize();
                    proj4.Normalize();
                    proj5.Normalize();
                    proj1 = proj1 * 6;
                    proj2 = proj2 * 6;
                    proj3 = proj3 * 6;
                    proj4 = proj4 * 6;
                    proj5 = proj5 * 6;

                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj1,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);


                }
                else if (AI_Timer == 120)
                {
                    Vector2 proj1 = new Vector2(0, 2);
                    Vector2 proj2 = new Vector2(-3, 2);
                    Vector2 proj3 = new Vector2(3, 2);
                    Vector2 proj4 = new Vector2(-2, -3);
                    Vector2 proj5 = new Vector2(2, -3);
                    proj1.Normalize();
                    proj2.Normalize();
                    proj3.Normalize();
                    proj4.Normalize();
                    proj5.Normalize();
                    proj1 = proj1 * 6;
                    proj2 = proj2 * 6;
                    proj3 = proj3 * 6;
                    proj4 = proj4 * 6;
                    proj5 = proj5 * 6;

                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj1,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                }
                else if (AI_Timer == 180)
                {
                    Vector2 proj1 = new Vector2(0, -2);
                    Vector2 proj2 = new Vector2(3, -2);
                    Vector2 proj3 = new Vector2(-3, -2);
                    Vector2 proj4 = new Vector2(2, 3);
                    Vector2 proj5 = new Vector2(-2, 3);
                    proj1.Normalize();
                    proj2.Normalize();
                    proj3.Normalize();
                    proj4.Normalize();
                    proj5.Normalize();
                    proj1 = proj1 * 6;
                    proj2 = proj2 * 6;
                    proj3 = proj3 * 6;
                    proj4 = proj4 * 6;
                    proj5 = proj5 * 6;

                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj1,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                }
                else if (AI_Timer == 240)
                {
                    Vector2 proj1 = new Vector2(0, 2);
                    Vector2 proj2 = new Vector2(-3, 2);
                    Vector2 proj3 = new Vector2(3, 2);
                    Vector2 proj4 = new Vector2(-2, -3);
                    Vector2 proj5 = new Vector2(2, -3);
                    proj1.Normalize();
                    proj2.Normalize();
                    proj3.Normalize();
                    proj4.Normalize();
                    proj5.Normalize();
                    proj1 = proj1 * 6;
                    proj2 = proj2 * 6;
                    proj3 = proj3 * 6;
                    proj4 = proj4 * 6;
                    proj5 = proj5 * 6;

                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj1,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj2,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj3,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj4,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj5,
                            ModContent.ProjectileType<HappyBoltNoTrail>(), 18, Projectile.knockBack, Projectile.owner);
                    Projectile.Kill();
                }
            }



        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        }
    }
}
