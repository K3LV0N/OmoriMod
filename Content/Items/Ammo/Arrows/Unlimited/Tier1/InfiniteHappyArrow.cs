using OmoriMod.Content.Items.Abstract_Classes;
using Terraria.ModLoader;
using OmoriMod.Content.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Content.Projectiles.Friendly.Arrows.Tier1.NoDrops;

namespace OmoriMod.Content.Items.Ammo.Arrows.Unlimited.Tier1
{
    public class InfiniteHappyArrow : HappyItem
    {
        InfiniteHappyArrow()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<InfiniteAngryArrow>(ModContent.ProjectileType<HappyArrowProjectileNoDrop>());
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<HappyArrow>());
        }
    }
}
