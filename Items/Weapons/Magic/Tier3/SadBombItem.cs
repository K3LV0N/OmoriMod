using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier3;
using OmoriMod.Items.Weapons.Magic.Tier2;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier3
{
    public class SadBombItem : SadItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // clone default weapon stuff
            Item.CloneDefaults(ModContent.ItemType<AngryBombItem>());

            // change projectile
            Item.shoot = ModContent.ProjectileType<SadBomb>();

            // sad item
            SetSadDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SadBundle>(), 1);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.Register();
        }

    }
}
