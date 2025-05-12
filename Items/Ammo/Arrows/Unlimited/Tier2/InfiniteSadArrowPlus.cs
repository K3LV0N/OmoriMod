using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier2
{
    public class InfiniteSadArrowPlus : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<InfiniteAngryArrowPlus>(ModContent.ProjectileType<SadArrowPlusProjectileNoDrop>());
            Item.damage = 50;
            Item.shootSpeed = 30f;
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<SadArrowPlus>());
        }
    }
}
