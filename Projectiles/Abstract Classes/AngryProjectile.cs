﻿using OmoriMod.Systems.EmotionSystem.Interfaces;

namespace OmoriMod.Projectiles.Abstract_Classes
{
    /// <summary>
    /// Automatically sets <see cref="EmotionProjectile.Emotion"/> to <see cref="EmotionType.ANGRY"/>.
    /// </summary>
    public abstract class AngryProjectile : EmotionProjectile
    {
        public AngryProjectile()
        {
            SetEmotionType(EmotionType.ANGRY);
        }
    }
}