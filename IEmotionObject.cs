﻿using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Items.Abstract_Classes;
using Terraria.ModLoader;
using Terraria;

namespace OmoriMod
{
    public enum EmotionType
    {
        NONE = 0,
        SAD = 1,
        ANGRY = 2,
        HAPPY = 3,
    }

    /// <summary>
    /// An interface to consolidate emotion application.
    /// Acts like an abstract class for <see cref="EmotionProjectile"/> and <see cref="EmotionItem"/>
    /// </summary>
    public interface IEmotionObject
    {
        

        /// <summary>
        /// The emotion this object inflicts when interacting with an enemy.
        /// </summary>
        protected EmotionType Emotion { get; }

        /// <summary>
        /// Inflicts an emotion determined by <see cref="Emotion"/> on an enemy.
        /// </summary>
        /// <param name="target">The <see cref="NPC"/> the emotion will be applied to.</param>
        /// <param name="ticks">The amount of ticks the emotion will be applied for.</param>
        public virtual void InflictEmotion(NPC target, int ticks = 600)
        {
            switch (Emotion)
            {
                case EmotionType.NONE:
                    break;
                case EmotionType.SAD:
                    if (!target.HasBuff<Happy>() && !target.HasBuff<Angry>())
                    {
                        target.AddBuff(ModContent.BuffType<Sad>(), ticks);
                    }
                    break;
                case EmotionType.ANGRY:
                    if (!target.HasBuff<Happy>() && !target.HasBuff<Sad>())
                    {
                        target.AddBuff(ModContent.BuffType<Angry>(), ticks);
                    }
                    break;
                case EmotionType.HAPPY:
                    if (!target.HasBuff<Angry>() && !target.HasBuff<Sad>())
                    {
                        target.AddBuff(ModContent.BuffType<Happy>(), ticks);
                    }
                    break;
            }
        }
    }
}
