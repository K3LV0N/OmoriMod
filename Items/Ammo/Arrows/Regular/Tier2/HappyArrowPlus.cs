using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Unlimited.Tier1;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop;

namespace OmoriMod.Items.Ammo.Arrows.Regular.Tier2
{
    public class HappyArrowPlus : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 99;
            // clone default arrow stuff
            Item.CloneDefaults(ModContent.ItemType<AngryArrowPlus>());

            // damage stuff
            Item.damage = ModContent.GetModItem(ModContent.ItemType<HappyArrow>()).Item.damage;

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<HappyArrowPlusProjectile>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe(100);
            recipe1.AddIngredient(ModContent.ItemType<HappyArrow>(), 100);
            recipe1.AddIngredient(ItemID.HallowedBar, 1);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe(100);
            recipe2.AddIngredient(ItemID.HallowedBar, 1);
            recipe2.AddCondition(Condition.PlayerCarriesItem(ModContent.ItemType<InfiniteHappyArrow>()));
            recipe2.Register();
        }
    }
}
