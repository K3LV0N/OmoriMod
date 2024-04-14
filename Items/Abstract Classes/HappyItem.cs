using Terraria.ID;

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
            emotion = emotionType.HAPPY;
        }

        /// <summary>
        /// <c>SetHappyDefaults</c> sets happy item defaults.<br />
        /// </summary>
        public void SetHappyDefaults()
        {
            Item.rare = ItemRarityID.Yellow;
        }
    }
}
