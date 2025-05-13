﻿using OmoriMod.Systems.EmotionSystem.Interfaces;

namespace OmoriMod.Items.Abstract_Classes
{
    /// <summary>
    /// Automatically sets <see cref="EmotionItem.Emotion"/> to <see cref="EmotionType.ANGRY"/>.
    /// </summary>
    public abstract class AngryItem : EmotionItem
    {
        public AngryItem()
        {
            SetEmotionType(EmotionType.ANGRY);
        }
    }
}
