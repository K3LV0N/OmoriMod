using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;

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
            HAPPY = 2
        }

        public emotionType emotion;

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            InflictEmotion(player, target);
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
            }
        }


    }
}
