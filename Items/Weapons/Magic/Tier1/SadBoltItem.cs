using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier1;
using OmoriMod.Items.BuffItems;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier1
{
    public class SadBoltItem : SadItem
    {
        public override void SetDefaults()
        {
            EmotionalItemCloneWithDifferentProjectile<AngryBoltItem>(ModContent.ProjectileType<SadBolt>());
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient<RainCloud>(10);
            recipe.Register();
        }

    }
}
