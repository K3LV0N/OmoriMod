using OmoriMod.Buffs.Abstract;

namespace OmoriMod.Systems.EmotionSystem.Interfaces
{
    public interface IEmotionEntity : IEmotionObject
    {

        /// <summary>
        /// Takes in an <see cref="EmotionBuff"/> and returns the corresponding <see cref="EmotionType"/>
        /// </summary>
        /// <typeparam name="T">The <see cref="EmotionBuff"/></typeparam>
        /// <returns>The corresponding <see cref="EmotionType"/></returns>
        public EmotionType IdentifyEmotion<T>() where T : EmotionBuff
        {
            if (typeof(AngryEmotionBase).IsAssignableFrom(typeof(T))) return EmotionType.ANGRY;
            if (typeof(HappyEmotionBase).IsAssignableFrom(typeof(T))) return EmotionType.HAPPY;
            if (typeof(SadEmotionBase).IsAssignableFrom(typeof(T))) return EmotionType.SAD;
            return EmotionType.NONE;
        }


        /// <summary>
        /// Determines whether an emotion of type <typeparamref name="T"/> can be applied 
        /// based on the current emotion state.
        /// </summary>
        /// <typeparam name="T">The type of the emotion to apply. Must inherit from <see cref="EmotionBuff"/>.</typeparam>
        /// <returns>
        /// <c>true</c> if the emotion can be applied (i.e., if no emotion is currently active,
        /// or if the type <typeparamref name="T"/> corresponds to the currently active emotion type); 
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool CanApplyEmotion<T>() where T : EmotionBuff
        {
            if (Emotion == EmotionType.NONE) return true;
            if (IdentifyEmotion<T>() == Emotion) return true;
            return false;
        }

        /// <summary>
        /// Applies the emotion specified to the <see cref="IEmotionEntity"/>
        /// </summary>
        /// <typeparam name="T">The <see cref="EmotionBuff"/> to apply.</typeparam>
        /// <param name="ticks">The ticks the <see cref="EmotionBuff"/> should last for. 
        /// If <paramref name="ticks"/> = -1, no timer will shown, and the buff will last forever.</param>
        public void ApplyEmotion<T>(int ticks) where T : EmotionBuff;
    }
}
