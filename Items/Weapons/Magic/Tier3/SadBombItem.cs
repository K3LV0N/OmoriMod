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
            EmotionItemCloneWithDifferentProjectile<AngryBombItem>(ModContent.ProjectileType<SadBomb>());
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
