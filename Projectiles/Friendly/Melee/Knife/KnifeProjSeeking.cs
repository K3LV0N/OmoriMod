using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Projectiles.Abstract_Classes;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Melee.Knife
{
    public class KnifeProjSeeking : SadProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = 0;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.timeLeft = 300;

            Projectile.alpha = 50;

            Projectile.arrow = false;
        }

        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public bool AI_TimerStartX = false;
        public bool AI_TimerStartY = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(AI_TimerStartX);
            writer.Write(AI_TimerStartY);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            AI_TimerStartX = reader.ReadBoolean();
            AI_TimerStartY = reader.ReadBoolean();
        }
        public override void AI()
        {

            Projectile.timeLeft--;
            if (Projectile.timeLeft <= 0)
            {
                Projectile.Kill();
            }

            AI_Timer++;
            if (Projectile.direction > 0)
            {
                Projectile.rotation += .5f;
            }
            else
            {
                Projectile.rotation -= .5f;
            }

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

            float maxDetectRadius = 800f; // The maximum radius at which a projectile can detect a target
            float XSpeed = (float)Math.Pow(Projectile.velocity.X, 2);
            float YSpeed = (float)Math.Pow(Projectile.velocity.Y, 2);
            float projSpeed = (float)Math.Pow(XSpeed + YSpeed, .5);

            // Trying to find NPC closest to the projectile
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            if (AI_Timer > 15)
            {
                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            }


            if (AI_TimerStartX && AI_TimerStartY)
            {
                if (AI_Timer > 60)
                {
                    AI_Timer = 0;
                }
                AI_Timer++;

                if (AI_Timer == 60)
                {
                    Projectile.Kill();
                }
            }


        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public override void OnKill(int timeleft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Blue);
        }

    }
}
