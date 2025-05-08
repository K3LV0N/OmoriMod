using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.BuffItems;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.CanDrop;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Regular.Tier1
{
    public class AngryArrow : AngryItem
    {
        public override void SetDefaults()
        {
            // consumability and stacks
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.ResearchUnlockCount = 99;
            Item.value = Item.buyPrice(0, 0, 1, 0);

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
            Item.shoot = ModContent.ProjectileType<AngryArrowProjectile>();

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe(50);
            recipe1.AddIngredient(ModContent.ItemType<AirHorn>(), 1);
            recipe1.AddIngredient(ItemID.WoodenArrow, 50);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe(50);
            recipe2.AddIngredient(ModContent.ItemType<AirHorn>(), 1);
            recipe2.AddCondition(Condition.PlayerCarriesItem(ItemID.EndlessQuiver));
            recipe2.Register();
        }
    }
}
