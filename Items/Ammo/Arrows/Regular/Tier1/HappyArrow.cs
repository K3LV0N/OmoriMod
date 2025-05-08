using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.BuffItems;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.CanDrop;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Regular.Tier1
{
    public class HappyArrow : HappyItem
    {
        public override void SetDefaults()
        {

            Item.ResearchUnlockCount = 99;
            // clone default arrow stuff
            Item.CloneDefaults(ModContent.ItemType<AngryArrow>());

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<HappyArrowProjectile>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe(50);
            recipe1.AddIngredient(ModContent.ItemType<PartyPopper>(), 1);
            recipe1.AddIngredient(ItemID.WoodenArrow, 50);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe(50);
            recipe2.AddIngredient(ModContent.ItemType<PartyPopper>(), 1);
            recipe2.AddCondition(Condition.PlayerCarriesItem(ItemID.EndlessQuiver));
            recipe2.Register();
        }
    }
}
