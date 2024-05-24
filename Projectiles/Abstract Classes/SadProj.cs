namespace OmoriMod.Projectiles.Abstract_Classes
{
    /// <summary>
    /// <c>SadProj</c> is a subclass of <see cref="EmotionProj"/> that automatically sets 
    /// <paramref name="emotion"/> to <paramref name="SAD"/>.<br />
    /// </summary>
    public abstract class SadProj : EmotionProj
    {
        public SadProj()
        {
            emotion = emotionType.SAD;
        }
    }
}