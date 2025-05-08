using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier3;
using OmoriMod.Items.Weapons.Magic.Tier2;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier3
{
    public class HappyBombItem : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBombItem>(ModContent.ProjectileType<HappyBomb>());
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
