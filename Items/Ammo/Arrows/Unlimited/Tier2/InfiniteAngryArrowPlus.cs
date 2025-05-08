using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier2
{
    public class InfiniteAngryArrowPlus : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 3;
            // consumability and stacks
            Item.consumable = false;
            Item.maxStack = 1;
            Item.ResearchUnlockCount = 3;
            Item.value = Item.buyPrice(0, 15, 0, 0);

            // combat
            Item.damage = 24;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.crit = 4;

            // size
            Item.width = 16;
            Item.height = 16;

            // projectile stuff
            Item.ammo = AmmoID.Arrow;
            Item.shootSpeed = 8.5f;
            Item.shoot = ModContent.ProjectileType<AngryArrowPlusProjectileNoDrop>();

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<AngryArrowPlus>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
