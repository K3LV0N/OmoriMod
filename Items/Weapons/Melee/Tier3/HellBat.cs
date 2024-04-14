using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier2;
using OmoriMod.Projectiles.Friendly.Melee.Bat;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier3
{
    public class HellBat : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // damage
            Item.damage = 35;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee;

            // projectiles
            Item.shoot = ModContent.ProjectileType<BatProjTriple>();
            Item.shootSpeed = 8f;

            // size
            Item.scale = 1.5f;
            Item.width = (int)(32 * Item.scale);
            Item.height = (int)(32 * Item.scale);

            // usage
            Item.useTime = 15;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            // price
            Item.value = Item.buyPrice(0, 4, 0, 0);

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CorruptionBat>(), 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<CrimsonBat>(), 1);
            recipe2.AddIngredient(ItemID.HellstoneBar, 15);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}
