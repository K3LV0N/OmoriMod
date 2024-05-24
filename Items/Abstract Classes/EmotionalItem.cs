using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;
using Microsoft.Xna.Framework;

namespace OmoriMod.Items.Abstract_Classes
{
    /// <summary>
    /// <para><c>EmotionalItem</c> is a class for weapons that inflict emotions</para> 
    /// To use, set the <paramref name="emotion"/> to the emotion the weapon inflicts. 
    /// Upon hitting an enemy, they will be inflicted with this emotion.<br />
    /// Valid <paramref name="emotion"/> values can be <paramref name="SAD"/>, <paramref name="ANGRY"/>, or <paramref name="HAPPY"/>.<br />
    /// </summary>
    public abstract class EmotionalItem : ModItem
    {
        public enum emotionType
        {
            SAD = 0,
            ANGRY = 1,
            HAPPY = 2,
            NOTHING = 3
        }

        public emotionType emotion = emotionType.NOTHING;

        public float meleeWeaponProjectileMoveTime = 0.2f;

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            InflictEmotion(player, target);
        }

        /// <summary>
        /// <c>MoveProjectileForward</c> moves a projectile shot by a weapon forward slightly to reduce spawn collisions. <br />
        /// <paramref name="position"/> is current position of the projectile. This <c>WILL</c>c be changed here.<br />
        /// <paramref name="velocity"/> is the current velocity of the projectile.<br />
        /// <paramref name="ticks"/> is the amount of ticks to move the projectile forward. Float for increased precision.<br />
        /// </summary>
        public virtual void MoveProjectileForward(ref Vector2 position, ref Vector2 velocity, float ticks = 2.1f)
        {   
            position = position + (velocity * ticks);
        }

        /// <summary>
        /// <c>InflictEmotion</c> inflicts an emotion on an enemy.<br />
        /// This function uses the field <paramref name="emotion"/> to determine the correct emotion to apply.<br /><br />
        /// <paramref name="player"/> is the player using the item.<br />
        /// <paramref name="target"/> is the NPC getting hit.<br />
        /// </summary>
        public virtual void InflictEmotion(Player player, NPC target)
        {
            switch (emotion)
            {
                case emotionType.SAD:
                    if (!target.HasBuff<Happy>() && !target.HasBuff<Angry>())
                    {
                        target.AddBuff(ModContent.BuffType<Sad>(), 600);
                    }
                    break;
                case emotionType.ANGRY:
                    if (!target.HasBuff<Happy>() && !target.HasBuff<Sad>())
                    {
                        target.AddBuff(ModContent.BuffType<Angry>(), 600);
                    }
                    break;
                case emotionType.HAPPY:
                    if (!target.HasBuff<Angry>() && !target.HasBuff<Sad>())
                    {
                        target.AddBuff(ModContent.BuffType<Happy>(), 600);
                    }
                    break;
                case emotionType.NOTHING:
                    break;
            }
        }


    }
}
