using Terraria;

namespace OmoriMod.Systems.AbilitySystem.ItemAbilities.AbilityContexts
{
    public class AbilityContext
    {
        public Player Player { get; set; }
        public Item Item { get; set; }

        public AbilityContext(Player player, Item item)
        {
            Player = player;
            Item = item;
        }
    }
}
