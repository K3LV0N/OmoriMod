using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Items.Ammo.Arrows.Unlimited.Tier1;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Regular.Tier2
{
    public class SadArrowPlus : SadItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 99;
            // clone default arrow stuff
            Item.CloneDefaults(ModContent.ItemType<AngryArrowPlus>());

            // changes
            Item.damage = 50;
            Item.shootSpeed = 30f;

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<SadArrowPlusProj>();

            // sad item
            SetSadDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe(100);
            recipe1.AddIngredient(ModContent.ItemType<SadArrow>(), 100);
            recipe1.AddIngredient(ItemID.HallowedBar, 1);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe(100);
            recipe2.AddIngredient(ItemID.HallowedBar, 1);
            recipe2.AddCondition(Condition.PlayerCarriesItem(ModContent.ItemType<InfiniteSadArrow>()));
            recipe2.Register();
        }
    }
}
