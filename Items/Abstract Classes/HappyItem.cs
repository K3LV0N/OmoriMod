
namespace OmoriMod.Items.Abstract_Classes
{
    /// <summary>
    /// <c>HappyItem</c> is a subclass of <see cref="EmotionalItem"/> that automatically sets 
    /// <paramref name="emotion"/> to <paramref name="HAPPY"/>.<br />
    /// </summary>
    public abstract class HappyItem : EmotionalItem
    {
        public HappyItem()
        {
            SetEmotionType(EmotionType.HAPPY);
        }
    }
}
