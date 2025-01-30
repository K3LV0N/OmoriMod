using Terraria;
using Terraria.ID;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Melee.Tier1
{
    public class FryingPan : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionalItemClone<Bat>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CopperBar, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.TinBar, 6);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}
