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
    public abstract class EmotionalItem : ModItem, IEmotionObject
    {
        public EmotionType Emotion { get; set; }

        /// <summary>
        /// Useful for when you need to manually set the emotion type
        /// </summary>
        /// <param name="emotion"></param>
        public void SetEmotionType(EmotionType emotion)
        {
            Emotion = emotion;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            ((IEmotionObject)this).InflictEmotion(target);
        }

        public float meleeWeaponProjectileMoveTime = 0.2f;

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
    }
}
