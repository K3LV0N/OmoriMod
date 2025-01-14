namespace OmoriMod.Projectiles.Abstract_Classes
{
    /// <summary>
    /// <c>AngryProj</c> is a subclass of <see cref="EmotionProj"/> that automatically sets 
    /// <paramref name="emotion"/> to <paramref name="ANGRY"/>.<br />
    /// </summary>
    public abstract class AngryProj : EmotionProj
    {
        public AngryProj()
        {
            SetEmotionType(EmotionType.ANGRY);
        }
    }
}