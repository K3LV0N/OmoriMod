namespace OmoriMod.Projectiles.Abstract_Classes
{
    /// <summary>
    /// <c>HappyProj</c> is a subclass of <see cref="EmotionProj"/> that automatically sets 
    /// <paramref name="emotion"/> to <paramref name="HAPPY"/>.<br />
    /// </summary>
    public abstract class HappyProj : EmotionProj
    {
        public HappyProj()
        {
            SetEmotionType(EmotionType.HAPPY);
        }
    }
}