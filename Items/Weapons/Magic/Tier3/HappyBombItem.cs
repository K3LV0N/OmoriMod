using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier3;
using OmoriMod.Items.Weapons.Magic.Tier2;
using OmoriMod.Projectiles.Friendly.Magic.Tier2;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier3
{
    public class HappyBombItem : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // clone default weapon stuff
            Item.CloneDefaults(ModContent.ItemType<AngryBombItem>());

            // change projectile
            Item.shoot = ModContent.ProjectileType<HappyBomb>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HappyBundle>(), 1);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.Register();


        }

    }
}
