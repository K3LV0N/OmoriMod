using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier2
{
    public class InfiniteHappyArrowPlus : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 3;
            // clone default arrow stuff
            Item.CloneDefaults(ModContent.ItemType<InfiniteAngryArrowPlus>());

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<HappyArrowPlusProjNoDrop>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<HappyArrowPlus>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
