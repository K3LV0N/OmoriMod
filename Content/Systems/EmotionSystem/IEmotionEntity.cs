using OmoriMod.Content.Buffs.Abstract;

namespace OmoriMod.Content.Systems.EmotionSystem;

public interface IEmotionEntity : IEmotionObject
{
    public bool ImmuneToEmotionChange { get; }
    public EmotionBuff ActiveEmotionBuff { get; set; }
    public int EmotionLevel { get; set; }

    /// <summary>
    /// Calculates which <see cref="IEmotionEntity"/> has the emotional advantage
    /// </summary>
    /// <param name="otherEntity"></param>
    /// <returns><c>true</c> if this <see cref="IEmotionEntity"/> has the advantage, 
    /// <c>false</c> if the other <see cref="IEmotionEntity"/> has the advantage,
    /// and <c>null</c> if there is no advantage
    /// </returns>
    public bool? CheckForAdvantage(IEmotionEntity otherEntity)
    {
        switch (Emotion)
        {
            case EmotionType.SAD:
                if (otherEntity.Emotion == EmotionType.HAPPY) return true;
                return otherEntity.Emotion == EmotionType.ANGRY ? false : null;
            case EmotionType.ANGRY:
                if (otherEntity.Emotion == EmotionType.SAD) return true;
                return otherEntity.Emotion == EmotionType.HAPPY ? false : null;
            case EmotionType.HAPPY:
                if (otherEntity.Emotion == EmotionType.ANGRY) return true;
                return otherEntity.Emotion == EmotionType.SAD ? false : null;
            default:
                return null;
        }
    }
}
