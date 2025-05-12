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
    /// An abstract class for projectiles that inflict emotions.
    /// Use <see cref="AngryProjectile"/>, <see cref="HappyProjectile"/>, or <see cref="SadProjectile"/> 
    /// to set emotions. If <see cref="Emotion"/> is not set, it will default to <see cref="EmotionType.NONE"/>.
    /// </summary>
    public abstract class EmotionProjectile : ModProjectile, IEmotionObject
    {
        /// <summary>
        /// The first value in the <see cref="Projectile.ai"/> array. Standardized for timers.
        /// </summary>
        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public EmotionType Emotion { get; protected set; }

        /// <summary>
        /// Used to set the <see cref="Emotion"/>
        /// </summary>
        /// <param name="emotion">The emotion to be set.</param>
        protected void SetEmotionType(EmotionType emotion)
        {
            Emotion = emotion;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // cast to the intertface to call the function
            ((IEmotionObject)this).InflictEmotion(target);
        }





        // DEFAULTS





        /// <summary>
        /// Set defaults for friendly projectiles.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <param name="damageType"></param>
        /// <param name="penetration"></param>
        /// <param name="tileCollide"></param>
        /// <param name="timeLeft"></param>
        /// <param name="alpha"></param>
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
        /// Sets the defaults for friendly modded arrows
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <param name="penetration"></param>
        /// <param name="tileCollide"></param>
        /// <param name="timeLeft"></param>
        /// <param name="alpha"></param>
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
        /// Sets the defaults for friendly modded bullets
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <param name="penetration"></param>
        /// <param name="tileCollide"></param>
        /// <param name="timeLeft"></param>
        /// <param name="alpha"></param>
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
        /// Set defaults for other friendly projectiles. Defaults to bullet AI.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="damageType"></param>
        /// <param name="aiStyle"></param>
        /// <param name="penetration"></param>
        /// <param name="scale"></param>
        /// <param name="tileCollide"></param>
        /// <param name="timeLeft"></param>
        /// <param name="alpha"></param>
        public void SetOtherDefaults(int width, int height, DamageClass damageType, int aiStyle, int penetration = 1, float scale = 1, bool tileCollide = true, int timeLeft = 3600, int alpha = 0)
        {
            // Set AI style
            Projectile.aiStyle = aiStyle;

            // Is not an arrow
            Projectile.arrow = false;

            FriendlyDefaults(width, height, scale, damageType, penetration, tileCollide, timeLeft, alpha);
        }





        // DUST AND DROPS





        /// <summary>
        /// Creates a new <see cref="EmotionDust"/>, with its color determined by <see cref="Emotion"/>
        /// </summary>
        public void MakeDust()
        {
            switch (Emotion)
            {
                case EmotionType.NONE:
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

        /// <summary>
        /// Creates the <see cref="ModItem"/> corresponding with the <see cref="EmotionProjectile"/> with a certain chance.
        /// </summary>
        /// <typeparam name="T">The <see cref="ModItem"/> for the corresponding <see cref="EmotionProjectile"/></typeparam>
        /// <param name="chance">The chance for the <see cref="ModItem"/> to drop. <paramref name="chance"/> = 5 means a 1 in 5 chance.</param>
        private void DropChance<T>(int chance = 5) where T : ModItem
        {
            if (Projectile.owner == Main.myPlayer)
            {
                //has a chance to drop arrow for pickup
                int item = Main.rand.NextBool(chance) ? Item.NewItem(Entity.GetSource_Death(), Projectile.getRect(), ModContent.ItemType<T>()) : 0;
            }
        }

        /// <summary>
        /// The <see cref="ModProjectile.OnKill(int)"/> method for modded projectiles that can drop an item.
        /// </summary>
        /// <typeparam name="T">The <see cref="ModItem"/> for the corresponding <see cref="EmotionProjectile"/></typeparam>
        /// <param name="timeLeft">How much time a projectile has left.</param>
        /// <param name="noSound">If no sound should be played.</param>
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
        /// The <see cref="ModProjectile.OnKill(int)"/> method for modded projectiles that can not drop an item.
        /// </summary>
        /// <param name="timeLeft">How much time a projectile has left.</param>
        /// <param name="noSound">If no sound should be played.</param>
        public void OnKillNoDrop(int timeLeft = 1, bool noSound = false)
        {
            if (timeLeft > 0)
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                if (!noSound) { SoundEngine.PlaySound(SoundID.Item10, Projectile.position); }
            }
        }





        // ROTATION AND GRAVITY HELPER METHODS





        /// <summary>
        /// A helper method for gravity.
        /// </summary>
        /// <param name="grav"> The amount of gravity applied.</param>
        /// <param name="terminalVelocity">Maximum Y speed.</param>
        private void TheGravityOfTheSituation(float grav, float terminalVelocity)
        {
            if (Projectile.velocity.Y > terminalVelocity)
                Projectile.velocity.Y = terminalVelocity;

            if (Projectile.velocity.Y < terminalVelocity)
                Projectile.velocity.Y += grav;
        }

        /// <summary>
        /// Rotates the Projectile by the velocity.
        /// If <paramref name="flip"/> is set to true, the Projectile will be flipped.
        /// </summary>
        /// <param name="flip">Whether the rotation should be flipped.</param>
        public void VelocityRotate(bool flip)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (flip) { Projectile.rotation += MathHelper.PiOver2; }
        }

        /// <summary>
        /// Rotated the Projectile based on its direction.
        /// </summary>
        /// <param name="rotation">The amount of rotation applied to the projectile.</param>
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





        // SPLITTING HELPER METHODS





        /// <summary>
        /// Splits 1 projectile into many projectiles.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ModProjectile"/> to be created.</typeparam>
        /// <param name="ProjectileAmount">The amount of projectiles to create.</param>
        /// <param name="damage">How much damage each created projectile does.</param>
        /// <param name="maxAngle">The maximum angle from the horizontal where projectiles may be created.</param>
        /// <param name="startingSpeed">The speed of each created projectile.</param>
        /// <param name="knockBack">The knockback of each created projectile.</param>
        /// <param name="kill">Whether or no the original projectile should be killed.</param>
        public virtual void SetSplit<T>(int ProjectileAmount, int damage, int maxAngle, float startingSpeed, float knockBack, bool kill = true) where T : ModProjectile
        {
            // make sure to only run this code for the projectile owner
            if (Main.myPlayer == Projectile.owner)
            {
                HashSet<int> angles = new HashSet<int>();

                int updatedProjectileAmount = ProjectileAmount;

                if (ProjectileAmount % 2 == 1)
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
                    Vector2 velocity = new Vector2(Projectile.velocity.X, Projectile.velocity.Y);
                    float angle = MathHelper.ToRadians(angleDegrees);
                    Matrix matrix = Matrix.CreateRotationZ(angle);

                    velocity = Vector2.Transform(velocity, matrix);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velocity, ModContent.ProjectileType<T>(), damage, knockBack);
                }

                if (kill) { Projectile.Kill(); }
            }
        }

        /// <summary>
        /// Helper method to spawn extra projectiles specifically for volley projectile AIs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="damagePerProjectile"></param>
        /// <param name="ProjectileSpeedOnSpawn"></param>
        /// <param name="shotsPerVolley"></param>
        /// <param name="interval"></param>
        /// <param name="flipAngle"></param>
        private void VolleyProjectileSpawning<T>(int damagePerProjectile, int ProjectileSpeedOnSpawn, int shotsPerVolley, int interval, float flipAngle) where T : ModProjectile
        {
            // make sure to only run this code for the projectile owner
            if (Main.myPlayer == Projectile.owner)
            {
                if (AI_Timer % interval == 0)
                {
                    // only implement flip if a flip angle is specified and if it is time for a flip
                    float currentFlipAngle = 0;
                    if (flipAngle != 0 && AI_Timer % (interval * 2) == 0)
                    {
                        currentFlipAngle = flipAngle;
                    }

                    Vector2[] spawnedProjectiles = GenerateCircleOfUnitVectors(shotsPerVolley, currentFlipAngle);
                    foreach (Vector2 vector in spawnedProjectiles)
                    {
                        // Normalize and scale
                        Vector2 velocity = vector * ProjectileSpeedOnSpawn;
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velocity,
                            ModContent.ProjectileType<T>(), damagePerProjectile, Projectile.knockBack, Projectile.owner);
                    }
                }
            }
        }

        /// <summary>
        /// Helper method to generate a circle of unit vectors.
        /// </summary>
        /// <param name="count">The amount of vectors generated.</param>
        /// <param name="flipAngle">The angle that the vector is flipped by.</param>
        /// <returns>A list of the generated unit vectors.</returns>
        private Vector2[] GenerateCircleOfUnitVectors(int count, float flipAngle = 0)
        {
            Vector2[] vecs = new Vector2[count];
            // Full circle divided by Projectile count
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





        // ANIMATION HELPER METHODS





        /// <summary>
        /// Statically set offsets for a shake animation.
        /// </summary>
        private static readonly Vector2[] ShakeOffsets =
        {
            new(1, 0), new(0, 1), new(-1, 1), new(-1, -1), new(1, -1)
        };

        /// <summary>
        /// Helper method to shake the center of a projectile.
        /// </summary>
        private void ShakeCenter()
        {
            Vector2 offset = ShakeOffsets[(int)AI_Timer % ShakeOffsets.Length];
            Projectile.Center += offset;
            Projectile.rotation += ((int)AI_Timer % 20) * 0.1f;
        }





        // OTHER HELPER METHODS





        /// <summary>
        /// Slows a Projectile down until it gets below the <paramref name="zeroThreshold"/>. Then the speed gets set to 0.
        /// </summary>
        /// <param name="slowPercentage">The percentage of speed that should be retained.</param>
        /// <param name="zeroThreshold">The speed in which triggers the velocity to be set to 0.</param>
        private void SlowProjectile(float slowPercentage, float zeroThreshold)
        {

            if (Math.Abs(Projectile.velocity.Length()) > zeroThreshold)
            {
                Projectile.velocity *= slowPercentage;
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
            }
        }

        /// <summary>
        /// A helper method that finds the closest NPC within the <paramref name="maxDetectDistance"/>. If no NPC is found, null is returned
        /// </summary>
        /// <param name="maxDetectDistance">The maximum distance that an NPC can be from the player to be returned by this algorithm.</param>
        /// <returns></returns>
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





        // AIS





        /// <summary>
        /// The AI for seeking scythe projectiles.
        /// </summary>
        /// <param name="ticksStationaryUntilDespawn">The amount of ticks that the scythe can be motionless before despawning.</param>
        /// <param name="rotation">The rotation per tick the scythe will rotate.</param>
        /// <param name="seekingDistance">The distance in which the scythe will seek out targets.</param>
        public void AI_SeekingScytheProjectile(int ticksStationaryUntilDespawn, float rotation, int seekingDistance)
        {
            AI_Timer++;
            RotateBasedOnDirection(rotation: rotation);
            SlowProjectile(slowPercentage: 0.97f, zeroThreshold: 0.5f);

            float maxDetectRadius = seekingDistance; // The maximum radius at which a Projectile can detect a target
            float XSpeed = (float)Math.Pow(Projectile.velocity.X, 2);
            float YSpeed = (float)Math.Pow(Projectile.velocity.Y, 2);
            float ProjectileSpeed = (float)Math.Pow(XSpeed + YSpeed, .5);

            // Trying to find NPC closest to the Projectile
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the Projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            if (AI_Timer > 15)
            {
                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * ProjectileSpeed;
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

        /// <summary>
        /// The AI for scythe projectiles.
        /// </summary>
        /// <param name="ticksStationaryUntilDespawn">The amount of ticks that the scythe can be motionless before despawning.</param>
        /// <param name="rotation">The rotation per tick the scythe will rotate.</param>
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

        /// <summary>
        /// The AI for upgraded angry heat seeking projectiles.
        /// </summary>
        public void AI_AngryHeatSeekingProjectile()
        {
            AI_Timer++;
            float maxDetectRadius = 500f; // The maximum radius at which a Projectile can detect a target
            float ProjectileSpeed = 20.5f; // The speed at which the Projectile moves towards the target

            // Trying to find NPC closest to the Projectile
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the Projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            if (AI_Timer > 15)
            {
                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * ProjectileSpeed;
            }
        }

        /// <summary>
        /// The AI for projectiles that split multiple times.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ModProjectile"/> to be created.</typeparam>
        /// <param name="maxAngle">The maximum angle from the horizontal where projectiles may be created.</param>
        /// <param name="ProjectileAmount">The amount of projectiles to create.</param>
        /// <param name="firstSplitDelay">The delay between the splitting starting.</param>
        /// <param name="splitTimer">The time between splits.</param>
        /// <param name="splits">How many times this projectile will split.</param>
        public void AI_MultiSplittingProjectile<T>(int maxAngle, int ProjectileAmount, int firstSplitDelay = 10, int splitTimer = 10, int splits = 2) where T : ModProjectile
        {
            float newAITime = AI_Timer - firstSplitDelay;
            if (AI_Timer >= firstSplitDelay && newAITime <= splitTimer * splits)
            {
                if (newAITime % splitTimer == 0)
                {
                    float speed = Projectile.velocity.Length();
                    SetSplit<T>(ProjectileAmount, Projectile.damage, maxAngle, speed, Projectile.knockBack, false);
                }
                
            }
            AI_Timer++;
        }

        /// <summary>
        /// The AI for projctiles that split.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ModProjectile"/> to be created.</typeparam>
        /// <param name="maxAngle">The maximum angle from the horizontal where projectiles may be created.</param>
        /// <param name="ProjectileAmount">The amount of projectiles to create.</param>
        public void AI_SplittingProjectile<T>(int maxAngle, int ProjectileAmount) where T : ModProjectile
        {
            if (AI_Timer == 0)
            {
                float speed = Projectile.velocity.Length();
                SetSplit<T>(ProjectileAmount, Projectile.damage, maxAngle, speed, Projectile.knockBack);
            }
            AI_Timer++;
        }

        /// <summary>
        /// The AI for custom speed projectiles.
        /// </summary>
        /// <param name="totalSpeed">Maximum speed the projectile can go.</param>
        /// <param name="gravGiven">The amount of gravity the projectile can experience. Ignored if <paramref name="gravity"/> is False.</param>
        /// <param name="gravity">Whenther the projectile experiences gravity or not.</param>
        public virtual void AI_SetSpeedProjectile(float totalSpeed, float gravGiven, bool gravity)
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

        /// <summary>
        /// The AI for volley projectiles.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="damagePerProjectile"></param>
        /// <param name="ProjectileSpeedOnSpawn"></param>
        /// <param name="volleys"></param>
        /// <param name="shotsPerVolley"></param>
        /// <param name="interval"></param>
        /// <param name="bundle"></param>
        /// <param name="flipAngle"></param>
        private void VolleyProjectileAI<T>(int damagePerProjectile, int ProjectileSpeedOnSpawn, int volleys, int shotsPerVolley, int interval, bool bundle, float flipAngle = 0) where T : ModProjectile
        {
            AI_Timer++;
            // only shake if bundle AI
            if (bundle) { ShakeCenter(); }
            VolleyProjectileSpawning<T>(damagePerProjectile, ProjectileSpeedOnSpawn, shotsPerVolley, interval, flipAngle);
            // all volleys done
            if (AI_Timer > interval * volleys) { Projectile.Kill(); }
        }

        public void AI_MagicBombProjectile<T>(int damagePerProjectile, int ProjectileSpeedOnSpawn, int volleys = 6, int shotsPerVolley = 8, int interval = 30) where T : ModProjectile
        {
            VolleyProjectileAI<T>(damagePerProjectile, ProjectileSpeedOnSpawn, volleys, shotsPerVolley, interval, bundle: false);
        }

        public void AI_MagicBombProjectileWithFlip<T>(int damagePerProjectile, int ProjectileSpeedOnSpawn, int volleys = 6, int shotsPerVolley = 8, int interval = 30) where T : ModProjectile
        {
            VolleyProjectileAI<T>(damagePerProjectile, ProjectileSpeedOnSpawn, volleys, shotsPerVolley, interval, bundle: false, flipAngle: MathHelper.PiOver4);
        }
        public void AI_BundledProjectile<T>(int damagePerProjectile, int ProjectileSpeedOnSpawn, int volleys = 4, int shotsPerVolley = 5, int interval = 60) where T : ModProjectile
        {
            if (AI_Timer == 0)
            {
                Projectile.Center = Main.MouseWorld;
                Projectile.velocity = Vector2.Zero;
                Projectile.netUpdate = true;
            }

            VolleyProjectileAI<T>(damagePerProjectile, ProjectileSpeedOnSpawn, volleys, shotsPerVolley, interval, bundle: true, flipAngle: MathHelper.PiOver2);
        }

        public void AI_TravelingBundleProjectile<T>(int damagePerProjectile, int ProjectileSpeedOnSpawn, int volleys = 4, int shotsPerVolley = 5, int interval = 60) where T : ModProjectile
        {
            SlowProjectile(slowPercentage: 0.97f, zeroThreshold: 0.5f);
            if (Projectile.velocity == Vector2.Zero) {
                VolleyProjectileAI<T>(damagePerProjectile, ProjectileSpeedOnSpawn, volleys, shotsPerVolley, interval, bundle: true, flipAngle: MathHelper.PiOver2);
            }
        }
    }
}