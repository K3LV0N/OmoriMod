using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Starter
{
    public class Note : ModItem
    {
        public override void SetDefaults()
        {
            // size
            Item.width = 32;
            Item.height = 32;

            // usage
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;

            // rarity
            Item.rare = ItemRarityID.Green;

            // price
            Item.value = Item.buyPrice(0, 0, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe test = CreateRecipe();
            test.Register();
        }
    }
}
