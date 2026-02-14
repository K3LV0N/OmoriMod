using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace OmoriMod.Systems.AbilitySystem
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
