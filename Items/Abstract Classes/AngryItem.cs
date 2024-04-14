using Terraria.ID;

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
            emotion = emotionType.ANGRY;
        }

        /// <summary>
        /// <c>SetAngryDefaults</c> sets angry item defaults.<br />
        /// </summary>
        public void SetAngryDefaults()
        {
            Item.rare = ItemRarityID.Red;
        }
    }
}
