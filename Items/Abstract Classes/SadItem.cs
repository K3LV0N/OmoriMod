using Terraria.ID;

namespace OmoriMod.Items.Abstract_Classes
{
    /// <summary>
    /// <c>SadItem</c> is a subclass of <see cref="EmotionalItem"/> that automatically sets 
    /// <paramref name="emotion"/> to <paramref name="SAD"/>.<br />
    /// </summary>
    public abstract class SadItem : EmotionalItem
    {
        public SadItem()
        {
            SetEmotionType(EmotionType.SAD);
        }

        /// <summary>
        /// <c>SetSadDefaults</c> sets sad item defaults.<br />
        /// </summary>
        public void SetSadDefaults()
        {
            Item.rare = ItemRarityID.Blue;
        }
    }
}
