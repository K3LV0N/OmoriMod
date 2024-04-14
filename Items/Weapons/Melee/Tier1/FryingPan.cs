using OmoriMod.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier1
{
    public class FryingPan : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // clone default weapon stuff
            Item.CloneDefaults(ModContent.ItemType<Bat>());

            // happy item
            SetHappyDefaults();
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
