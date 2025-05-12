using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier1
{
    public class InfiniteSadArrow : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<InfiniteAngryArrow>(ModContent.ProjectileType<SadArrowProjectileNoDrop>());
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<SadArrow>());
        }
    }
}
