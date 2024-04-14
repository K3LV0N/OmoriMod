using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier3;
using OmoriMod.Projectiles.Friendly.Melee.Bat;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier4
{
    public class HallowBat : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // damage
            Item.damage = 50;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee;

            // projectiles
            Item.shoot = ModContent.ProjectileType<BatProjFive>();
            Item.shootSpeed = 8f;

            // size
            Item.scale = 1.5f;
            Item.width = (int)(32 * Item.scale);
            Item.height = (int)(32 * Item.scale);

            // usage
            Item.useTime = 14;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            // price
            Item.value = Item.buyPrice(0, 8, 0, 0);

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HellBat>(), 1);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
