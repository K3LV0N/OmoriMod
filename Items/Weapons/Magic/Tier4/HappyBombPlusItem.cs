using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Weapons.Magic.Tier3;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Magic.Tier4;

namespace OmoriMod.Items.Weapons.Magic.Tier4
{
    public class HappyBombPlusItem : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionalItemCloneWithDifferentProjectile<AngryBombPlusItem>(ModContent.ProjectileType<HappyBombPlus>());
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HappyBombItem>(), 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.Register();
        }

    }
}
