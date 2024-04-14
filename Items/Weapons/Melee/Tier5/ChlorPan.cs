using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier4;
using OmoriMod.Projectiles.Friendly.Melee.Pan;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier5
{
    public class ChlorPan : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // clone default weapon stuff
            Item.CloneDefaults(ModContent.ItemType<ChlorBat>());

            // change projectile
            Item.shoot = ModContent.ProjectileType<PanProjFiveSeeking>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HallowPan>(), 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
