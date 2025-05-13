using OmoriMod.Buffs.Abstract;
using OmoriMod.Systems.EmotionSystem.Interfaces;
using Terraria.ModLoader;

namespace OmoriMod.Players
{
    public class EmotionPlayer : ModPlayer, IEmotionEntity
    {
        public EmotionType Emotion { get; set; }

        public override void ResetEffects()
        {
            Emotion = EmotionType.NONE;
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
            if (((IEmotionEntity)this).IdentifyEmotion<T>() == Emotion) return true;
            return false;
        }

        public void ApplyEmotion<T>(int ticks) where T : EmotionBuff {
            Emotion = ((IEmotionEntity)this).IdentifyEmotion<T>();

            Player.AddBuff(
                type: ModContent.BuffType<T>(),
                timeToAdd: ticks
                );
        }
    }
}
