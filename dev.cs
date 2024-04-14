using Terraria.ModLoader;
using Terraria;

namespace OmoriMod
{
    internal class dev : ModSystem
    {
        private bool devBranch = false;

        public override void AddRecipes()
        {
            // Allows creation of any item from Omori Mod
            // to be made from thin air
            if (devBranch)
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
