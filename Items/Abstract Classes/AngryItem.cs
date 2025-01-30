
namespace OmoriMod.Items.Abstract_Classes
{
    /// <summary>
    /// <c>AngryItem</c> is a subclass of <see cref="EmotionalItem"/> that automatically sets 
    /// <paramref name="emotion"/> to <paramref name="ANGRY"/>.<br />
    /// </summary>
    public abstract class AngryItem : EmotionalItem
    {
        public AngryItem()
        {
            SetEmotionType(EmotionType.ANGRY);
        }
    }
}
