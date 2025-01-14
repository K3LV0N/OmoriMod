using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace OmoriMod.Summons.Abstract_Classes
{
    public abstract class ModPetItem : ModItem
    {
        // sets defaults for a pet item
        public void SetPetDefaults()
        {
            // Research cost for pets
            Item.ResearchUnlockCount = 1;

            // Pet summoning style
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item2;

            Item.useTime = 20;
            Item.useAnimation = Item.useTime;

            // Cost for pets
            Item.value = Item.buyPrice(platinum: 1, gold: 0, silver: 0, copper: 0);
        }
    }
}
