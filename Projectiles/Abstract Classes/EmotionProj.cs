using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Dusts;

namespace OmoriMod.Projectiles.Abstract_Classes
{
    /// <summary>
    /// <para><c>EmotionProj</c> is a class for projectiles that inflict emotions</para> 
    /// To use, set the <paramref name="emotion"/> to the emotion the weapon inflicts. 
    /// Upon hitting an enemy, they will be inflicted with this emotion.<br />
    /// Valid <paramref name="emotion"/> values can be <paramref name="SAD"/>, <paramref name="ANGRY"/>, or <paramref name="HAPPY"/>.<br />
    /// </summary>
    public abstract class EmotionProj : ModProjectile, IEmotionObject
    {
        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public EmotionType Emotion { get; set; }

        /// <summary>
        /// Useful for when you need to manually set the emotion type
        /// </summary>
        /// <param name="emotion"></param>
        public void SetEmotionType(EmotionType emotion)
        {
            Emotion = emotion;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // cast to the intertface to call the function
            ((IEmotionObject)this).InflictEmotion(target);
        }

        private void FriendlyDefaults(int width, int height, float scale, DamageClass damageType, int penetration, bool tileCollide, int timeLeft, int alpha)
        {
            // Friendly so it hurts enemies
            Projectile.friendly = true;

            // Dimensions
            Projectile.scale = scale;
            Projectile.width = (int)(width * Projectile.scale);
            Projectile.height = (int)(height * Projectile.scale);


            // Ranged damage
            Projectile.DamageType = damageType;

            Projectile.tileCollide = tileCollide;

            Projectile.timeLeft = timeLeft;

            Projectile.alpha = alpha;
        }

        /// <summary>
        /// Sets the defaults for modded arrows
        /// </summary>
        public void SetArrowDefaults(int width = 8, int height = 12, float scale = 1, int penetration = 1, bool tileCollide = true, int timeLeft = 3600, int alpha = 0)
        {
            // Copy the ai style of an arrow
            Projectile.aiStyle = ProjAIStyleID.Arrow;

            // Is an arrow
            Projectile.arrow = true;

            // Ranged damage type
            DamageClass damageType = DamageClass.Ranged;


            FriendlyDefaults(width, height, scale, damageType, penetration, tileCollide, timeLeft, alpha);
        }

        /// <summary>
        /// Sets the defaults for modded bullets
        /// </summary>
        public void SetBulletDefaults(int width = 6, int height = 6, float scale = 1, int penetration = 1, bool tileCollide = true, int timeLeft = 3600, int alpha = 0)
        {
            // Copy the ai style of a bullet
            Projectile.aiStyle = 0;

            // Is not an arrow
            Projectile.arrow = false;

            // Ranged damage type
            DamageClass damageType = DamageClass.Ranged;

            FriendlyDefaults(width, height, scale, damageType, penetration, tileCollide, timeLeft, alpha);
        }

        /// <summary>
        /// Defaults to bullet AI
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <param name="aiStyle"></param>
        public void SetOtherDefaults(int width, int height, DamageClass damageType, int aiStyle, int penetration = 1, float scale = 1, bool tileCollide = true, int timeLeft = 3600, int alpha = 0)
        {
            // Set AI style
            Projectile.aiStyle = aiStyle;

            // Is not an arrow
            Projectile.arrow = false;

            FriendlyDefaults(width, height, scale, damageType, penetration, tileCollide, timeLeft, alpha);
        }

        /// <summary>
        /// Makes a dust trail behind the projectile with the color determined from the emotion
        /// </summary>
        public void DustTrail()
        {
            switch (Emotion)
            {
                case EmotionType.NOTHING:
                    Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.White);
                    break;
                case EmotionType.SAD:
                    Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Blue);
                    break;
                case EmotionType.ANGRY:
                    Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Red);
                    break;
                case EmotionType.HAPPY:
                    Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Yellow);
                    break;

            }
        }


        private void DropChance<T>() where T : ModItem
        {
            if (Projectile.owner == Main.myPlayer)
            {
                //has a chance to drop arrow for pickup
                int item = Main.rand.NextBool(5) ? Item.NewItem(Entity.GetSource_Death(), Projectile.getRect(), ModContent.ItemType<T>()) : 0;
            }
        }

        /// <summary>
        /// The OnKill Method for arrows that can drop
        /// </summary>
        public void OnKillWithDrop<T>(int timeLeft = 1, bool noSound = false) where T : ModItem
        {
            if (timeLeft > 0)
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                if (!noSound) { SoundEngine.PlaySound(SoundID.Item10, Projectile.position); }
            }
            DropChance<T>();
        }

        /// <summary>
        /// The OnKill Method anything that can't drop
        /// </summary>
        public void OnKillNoDrop(int timeLeft = 1, bool noSound = false)
        {
            if (timeLeft > 0)
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                if (!noSound) { SoundEngine.PlaySound(SoundID.Item10, Projectile.position); }
            }
        }

        /// <summary>
        /// <para><c>SetSplit</c> is a function that splits 1 projectile into many</para> 
        /// <typeparamref name="T"/> is the class of the projectiles that are being created.<br />
        /// <paramref name="target"/> is the enemy hit.<br />
        /// <paramref name="spawnedProjectileAmount"/> is the total amount of projectiles<br />
        /// <paramref name="damage"/> is how much damage each projectile does.<br />
        /// <paramref name="maxAngle"/> is the maximum angle from the horizontal that projectiles may be created.<br />
        /// <paramref name="startingSpeed"/> is the speed of each created projectile.<br />
        /// <paramref name="knockBack"/> is the knockback of each projectile.<br />
        /// <paramref name="kill"/> set to true if this function should kill the original projectile.<br />
        /// </summary>
        public virtual void SetSplit<T>(int projectileAmount, int damage, int maxAngle, float startingSpeed, float knockBack, bool kill=true) where T : ModProjectile
        {

            HashSet<int> angles = new HashSet<int>();

            int updatedProjectileAmount = projectileAmount;

            if(projectileAmount % 2 == 1)
            {
                updatedProjectileAmount--;

                if (kill) { angles.Add(0); }
                    
            }

            // divide by 2 to do 1 side
            float angleDistances = (float)(maxAngle / (updatedProjectileAmount / 2));
            int currentAngle = 0;

            for (int i = 0; i < updatedProjectileAmount / 2; i++)
            {
                currentAngle += (int)(angleDistances);
                angles.Add(currentAngle);
                angles.Add(-currentAngle);
            }

            foreach (int angleDegrees in angles)
            {
                Vector2 proj = new Vector2(Projectile.velocity.X, Projectile.velocity.Y);
                float angle = MathHelper.ToRadians(angleDegrees);
                Matrix matrix = Matrix.CreateRotationZ(angle);

                proj = Vector2.Transform(proj, matrix);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, proj, ModContent.ProjectileType<T>(), damage, knockBack);
            }

            if (kill) { Projectile.Kill(); }
        }

        /// <summary>
        /// Slows a projectile down until it gets below the zero threshold, in which case the velocity for that direction is set to 0
        /// </summary>
        /// <param name="slowPercentage"></param>
        /// <param name="zeroThreshold"></param>
        public void SlowProjectile(float slowPercentage, float zeroThreshold)
        {
            if (Math.Abs(Projectile.velocity.X) > zeroThreshold)
            {
                Projectile.velocity.X *= slowPercentage;
            }
            else
            {
                Projectile.velocity.X = 0;
            }

            if (Math.Abs(Projectile.velocity.Y) > zeroThreshold)
            {
                Projectile.velocity.Y *= slowPercentage;
            }
            else
            {
                Projectile.velocity.Y = 0;
            }
        }

        /// <summary>
        /// Rotates the projectile by the velocity.
        /// 
        /// If <paramref name="flip"/> is set to true, the projectile will be flipped.
        /// </summary>
        /// <param name="flip"></param>
        public void VelocityRotate(bool flip)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (flip) { Projectile.rotation += MathHelper.PiOver2; }
        }

        public void RotateBasedOnDirection(float rotation)
        {
            if (Projectile.direction > 0)
            {
                Projectile.rotation += rotation;
            }
            else
            {
                Projectile.rotation -= rotation;
            }
        }

        public void AI_SeekingScytheProjectile(int ticksStationaryUntilDespawn, float rotation, int seekingDistance)
        {
            AI_Timer++;
            RotateBasedOnDirection(rotation: rotation);
            SlowProjectile(slowPercentage: 0.97f, zeroThreshold: 0.5f);

            float maxDetectRadius = seekingDistance; // The maximum radius at which a projectile can detect a target
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


            if (Projectile.velocity == Vector2.Zero)
            {
                if (AI_Timer > ticksStationaryUntilDespawn)
                {
                    AI_Timer = 0;
                }
                AI_Timer++;

                if (AI_Timer == ticksStationaryUntilDespawn)
                {
                    Projectile.Kill();
                }
            }
        }

        public void AI_ScytheProjectile(int ticksStationaryUntilDespawn, float rotation)
        {
            RotateBasedOnDirection(rotation: rotation);
            SlowProjectile(slowPercentage: 0.97f, zeroThreshold: 0.5f);
            if (Projectile.velocity == Vector2.Zero)
            {
                AI_Timer++;

                if (AI_Timer > ticksStationaryUntilDespawn)
                {
                    Projectile.Kill();
                }
            }
        }

        private NPC FindClosestNPC(float maxDetectDistance)
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

        public void AI_AngryHeatSeekingProj()
        {
            AI_Timer++;
            float maxDetectRadius = 500f; // The maximum radius at which a projectile can detect a target
            float projSpeed = 20.5f; // The speed at which the projectile moves towards the target

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
        }

        public void AI_MultiSplittingProj<T>(int maxAngle, int projectileAmount) where T : ModProjectile
        {
            float deathTime = 20;
            if (AI_Timer == 10 || AI_Timer == deathTime)
            {
                float speed = Projectile.velocity.Length();
                SetSplit<T>(projectileAmount, Projectile.damage, maxAngle, speed, Projectile.knockBack, false);
            }
            AI_Timer++;
        }
        public void AI_SplittingProj<T>(int maxAngle, int projectileAmount) where T : ModProjectile
        {
            if (AI_Timer == 0)
            {
                float speed = Projectile.velocity.Length();
                SetSplit<T>(projectileAmount, Projectile.damage, maxAngle, speed, Projectile.knockBack);
            }
            AI_Timer++;
        }

        private void TheGravityOfTheSituation(float grav, float terminalVelocity)
        {
            if (Projectile.velocity.Y > terminalVelocity)
                Projectile.velocity.Y = terminalVelocity;

            if (Projectile.velocity.Y < terminalVelocity)
                Projectile.velocity.Y += grav;
        }

        public virtual void AI_SetSpeedProj(float totalSpeed, float gravGiven, bool gravity)
        {

            Projectile.velocity.Normalize();
            Projectile.velocity *= totalSpeed;

            VelocityRotate(flip: true);

            // 0.1f for normal arrow gravity, 0.4f for knife gravity
            // float arrowGrav = 0.1f;
            // float knifeGrav = 0.4f;
            // float defaultTerminal = 16f;

            if (gravity)
            {
                TheGravityOfTheSituation(gravGiven, totalSpeed);
            }
        }

        private Vector2[] GenerateCircleOfUnitVectors(int count, float flipAngle = 0)
        {
            Vector2[] vecs = new Vector2[count];
            // Full circle divided by projectile count
            float angleStep = MathHelper.TwoPi / count;

            // Choose to flip only if a flip angle is specified
            float startAngle = flipAngle == 0 ? 3 * MathHelper.PiOver2 : flipAngle;

            for (int i = 0; i < count; i++)
            {
                float angle = startAngle + i * angleStep;
                // Unit vector on circle
                vecs[i] = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            }

            return vecs;
        }

        private static readonly Vector2[] ShakeOffsets =
        {
            new(1, 0), new(0, 1), new(-1, 1), new(-1, -1), new(1, -1)
        };

        private void ShakeCenter()
        {
            Vector2 offset = ShakeOffsets[(int)AI_Timer % ShakeOffsets.Length];
            Projectile.Center += offset;
            Projectile.rotation += ((int)AI_Timer % 20) * 0.1f;
        }

        private void VolleyProjectileAI<T>(int damagePerProjectile, int projectileSpeedOnSpawn, int volleys, int shotsPerVolley, int interval, bool bundle, float flipAngle = 0) where T : ModProjectile
        {
            AI_Timer++;
            // only shake if bundle AI
            if (bundle) { ShakeCenter(); }

            if (AI_Timer % interval == 0)
            {
                // only implement flip if a flip angle is specified and if it is time for a flip
                float currentFlipAngle = 0;
                if (flipAngle != 0 && AI_Timer % (interval * 2) == 0) { 
                    currentFlipAngle = flipAngle; 
                }

                Vector2[] spawnedProjectiles = GenerateCircleOfUnitVectors(shotsPerVolley, currentFlipAngle);
                foreach (Vector2 vector in spawnedProjectiles)
                {
                    // Normalize and scale
                    Vector2 velocity = vector * projectileSpeedOnSpawn;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velocity,
                        ModContent.ProjectileType<T>(), damagePerProjectile, Projectile.knockBack, Projectile.owner);
                }
            }

            // all volleys done
            if (AI_Timer > interval * volleys) { Projectile.Kill(); }
        }

        public void AI_MagicBombProjectile<T>(int damagePerProjectile, int projectileSpeedOnSpawn, int volleys = 6, int shotsPerVolley = 8, int interval = 30) where T : ModProjectile
        {
            VolleyProjectileAI<T>(damagePerProjectile, projectileSpeedOnSpawn, volleys, shotsPerVolley, interval, bundle: false);
        }

        public void AI_MagicBombProjectileWithFlip<T>(int damagePerProjectile, int projectileSpeedOnSpawn, int volleys = 6, int shotsPerVolley = 8, int interval = 30) where T : ModProjectile
        {
            VolleyProjectileAI<T>(damagePerProjectile, projectileSpeedOnSpawn, volleys, shotsPerVolley, interval, bundle: false, flipAngle: MathHelper.PiOver4);
        }
        public void AI_BundledProjectile<T>(int damagePerProjectile, int projectileSpeedOnSpawn, int volleys = 4, int shotsPerVolley = 5, int interval = 60) where T : ModProjectile
        {
            if (AI_Timer == 0)
            {
                Projectile.Center = Main.MouseWorld;
                Projectile.velocity = Vector2.Zero;
                Projectile.netUpdate = true;
            }

            VolleyProjectileAI<T>(damagePerProjectile, projectileSpeedOnSpawn, volleys, shotsPerVolley, interval, bundle: true, flipAngle: MathHelper.PiOver2);
        }

        public void AI_TravelingBundleProjectile<T>(int damagePerProjectile, int projectileSpeedOnSpawn, int volleys = 4, int shotsPerVolley = 5, int interval = 60) where T : ModProjectile
        {
            SlowProjectile(slowPercentage: 0.97f, zeroThreshold: 0.5f);
            if (Projectile.velocity == Vector2.Zero) {
                VolleyProjectileAI<T>(damagePerProjectile, projectileSpeedOnSpawn, volleys, shotsPerVolley, interval, bundle: true, flipAngle: MathHelper.PiOver2);
            }
        }
    }
}