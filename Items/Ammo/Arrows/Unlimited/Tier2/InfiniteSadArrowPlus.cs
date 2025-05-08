using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier2
{
    public class InfiniteSadArrowPlus : SadItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 3;
            // clone default arrow stuff
            Item.CloneDefaults(ModContent.ItemType<InfiniteAngryArrowPlus>());

            // changes
            Item.damage = 50;
            Item.shootSpeed = 30f;

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<SadArrowPlusProjectileNoDrop>();

            // sad item
            SetSadDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<SadArrowPlus>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
