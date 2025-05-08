using Terraria;
using Terraria.ID;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Melee.Tier1
{
    public class Knife : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemClone<Bat>();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.IronBar, 6);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.LeadBar, 6);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}