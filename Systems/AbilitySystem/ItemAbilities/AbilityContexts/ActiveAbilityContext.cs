using Terraria;

namespace OmoriMod.Systems.AbilitySystem.ItemAbilities.AbilityContexts
{
    public class ActiveAbilityContext : AbilityContext
    {
        // Add specific active ability properties here if needed.
        // For now, Player and Item are sufficient.
        // Maybe "IsChanneling"? or "Duration"?

        public ActiveAbilityContext(Player player, Item item) 
            : base(player, item)
        {
        }
    }
}
