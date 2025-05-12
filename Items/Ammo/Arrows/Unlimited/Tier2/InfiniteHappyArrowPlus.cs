using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier2
{
    public class InfiniteHappyArrowPlus : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<InfiniteAngryArrowPlus>(ModContent.ProjectileType<HappyArrowPlusProjectileNoDrop>());
            Item.damage = ModContent.GetModItem(ModContent.ItemType<HappyArrow>()).Item.damage;
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<HappyArrowPlus>());
        }
    }
}
