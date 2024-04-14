using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier1
{
    public class InfiniteAngryArrow : AngryItem
    {
        public override void SetDefaults()
        {
            // consumability and stacks
            Item.consumable = false;
            Item.maxStack = 1;
            Item.ResearchUnlockCount = 3;
            Item.value = Item.buyPrice(0, 5, 0, 0);

            // combat
            Item.damage = 14;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.crit = 4;

            // size
            Item.width = 16;
            Item.height = 16;

            // projectile stuff
            Item.ammo = AmmoID.Arrow;
            Item.shootSpeed = 8.5f;
            Item.shoot = ModContent.ProjectileType<AngryArrowProjNoDrop>();

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<AngryArrow>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
