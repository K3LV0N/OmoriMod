using OmoriMod.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier1
{
    public class Bat : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // damage
            Item.damage = 8;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee;

            // size
            Item.scale  = 1.5f;
            Item.width  = (int)(32 * Item.scale);
            Item.height = (int)(32 * Item.scale);

            // usage
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            // price
            Item.value = Item.buyPrice(0, 1, 0, 0);
            
            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
