using Terraria;

namespace OmoriMod.Items.Abstract_Classes
{
    public abstract class EmotionBuffItem : EmotionItem
    {
        public int cooldownTicks;
        public int timer;

        public EmotionBuffItem()
        {
            cooldownTicks = 45;
            timer = 0;
        }

        /// <summary>
        /// Use this instead of <see cref="UpdateInventory(Player)"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/></param>
        public virtual void UpdateInventoryEmotionBuffItem(Player player) { }

        /// <summary>
        /// Use this instead of <see cref="UseItem(Player)(Player)"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/></param>
        public virtual bool? UseItemEmotionBuffItem(Player player) { return null; }

        /// <summary>
        /// Use this instead of <see cref="CanUseItem(Player)(Player)"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/></param>
        public virtual bool CanUseItemEmotionBuffItem(Player player) { return true; }

        public override void UpdateInventory(Player player)
        {
            if (timer > 0) { timer--; }
            UpdateInventoryEmotionBuffItem(player);
        }

        public override bool? UseItem(Player player)
        {
            timer = cooldownTicks;
            return UseItemEmotionBuffItem(player);
        }

        public override bool CanUseItem(Player player)
        {
            bool timerBool = false;
            if (timer == 0) timerBool = true;
            return timerBool && CanUseItemEmotionBuffItem(player);
        }
    }
}
