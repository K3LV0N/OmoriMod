using Microsoft.Xna.Framework;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Abstract_Classes
{
    /// <summary>
    /// <para><c>EmotionProj</c> is a class for projectiles that inflict emotions</para> 
    /// To use, set the <paramref name="emotion"/> to the emotion the weapon inflicts. 
    /// Upon hitting an enemy, they will be inflicted with this emotion.<br />
    /// Valid <paramref name="emotion"/> values can be <paramref name="SAD"/>, <paramref name="ANGRY"/>, or <paramref name="HAPPY"/>.<br />
    /// </summary>
    public abstract class EmotionProj : ModProjectile
    {
        public enum emotionType
        {
            SAD = 0,
            ANGRY = 1,
            HAPPY = 2
        }

        public emotionType emotion;
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            InflictEmotion(target);
        }

        /// <summary>
        /// <c>InflictEmotion</c> inflicts an emotion on an enemy.<br />
        /// This function uses the field <paramref name="emotion"/> to determine the correct emotion to apply.<br /><br />
        /// <paramref name="player"/> is the player using the item.<br />
        /// <paramref name="target"/> is the NPC getting hit.<br />
        /// </summary>
        public virtual void InflictEmotion(NPC target)
        {
            if (emotion == emotionType.SAD)
            {
                if (!target.HasBuff<Happy>() && !target.HasBuff<Angry>())
                {
                    target.AddBuff(ModContent.BuffType<Sad>(), 600);
                }
            }
            else if (emotion == emotionType.ANGRY)
            {
                if (!target.HasBuff<Happy>() && !target.HasBuff<Sad>())
                {
                    target.AddBuff(ModContent.BuffType<Angry>(), 600);
                }
            }
            else if (emotion == emotionType.HAPPY)
            {
                if (!target.HasBuff<Angry>() && !target.HasBuff<Sad>())
                {
                    target.AddBuff(ModContent.BuffType<Happy>(), 600);
                }
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

    }

}
