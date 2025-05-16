using Terraria.ModLoader;
using Terraria;

namespace OmoriMod
{
    internal class Dev : ModSystem
    {
        // MAKE SURE THIS IS FALSE BEFORE PUBLISHING
        readonly private bool devMode = true;

        public override void AddRecipes()
        {
            // Allows creation of any item from Omori Mod
            // to be made from thin air
            if (devMode)
            {
                int maxItems = ItemLoader.ItemCount;
                for (int i = 0; i < maxItems; i++)
                {
                    ModItem item = ItemLoader.GetItem(i);

                    if (item != null && item.Mod is OmoriMod)
                    {
                        Recipe testRecipe = Recipe.Create(i, 1);
                        testRecipe.Register();

                    }
                }
            }
        }
    }
}
