using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;
using Terraria.ModLoader;
using Terraria;

namespace OmoriMod
{
    public enum EmotionType
    {
        NOTHING = 0,
        SAD = 1,
        ANGRY = 2,
        HAPPY = 3,
    }

    /// <summary>
    /// <para><c>EmotionObject</c> is a class for anything that inflicts emotions</para> 
    /// To use, set the <paramref name="emotion"/> to the emotion the object inflicts. 
    /// Upon hitting an enemy, they will be inflicted with this emotion.<br />
    /// Valid <paramref name="emotion"/> values can be <paramref name="NOTHING"/>, <paramref name="SAD"/>, <paramref name="ANGRY"/>, or <paramref name="HAPPY"/>.<br />
    /// </summary>
    public interface IEmotionObject
    {
        

        /// <summary>
        /// The emotion this object inflicts when interacting with an enemy.
        /// </summary>
        public EmotionType Emotion { get; set; }

        /// <summary>
        /// <c>InflictEmotion</c> inflicts an emotion on an enemy.<br />
        /// This function uses the field <paramref name="emotion"/> to determine the correct emotion to apply.<br /><br />
        /// <paramref name="player"/> is the player using the item.<br />
        /// <paramref name="target"/> is the NPC getting hit.<br />
        /// </summary>
        public virtual void InflictEmotion(NPC target, int ticks = 600)
        {
            switch (Emotion)
            {
                case EmotionType.NOTHING:
                    break;
                case EmotionType.SAD:
                    if (!target.HasBuff<Happy>() && !target.HasBuff<Angry>())
                    {
                        target.AddBuff(ModContent.BuffType<Sad>(), 600);
                    }
                    break;
                case EmotionType.ANGRY:
                    if (!target.HasBuff<Happy>() && !target.HasBuff<Sad>())
                    {
                        target.AddBuff(ModContent.BuffType<Angry>(), 600);
                    }
                    break;
                case EmotionType.HAPPY:
                    if (!target.HasBuff<Angry>() && !target.HasBuff<Sad>())
                    {
                        target.AddBuff(ModContent.BuffType<Happy>(), 600);
                    }
                    break;
                
            }
        }
    }
}
