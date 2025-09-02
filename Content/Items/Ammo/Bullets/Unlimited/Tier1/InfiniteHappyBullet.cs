using OmoriMod.Items.Abstract_Classes;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Projectiles.Friendly.Bullets.Tier1;
using OmoriMod.Content.Items.Ammo.Bullets.Regular.Tier1;

namespace OmoriMod.Content.Items.Ammo.Bullets.Unlimited.Tier1
{
    public class InfiniteHappyBullet : HappyItem
    {
        InfiniteHappyBullet()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<InfiniteAngryBullet>(ModContent.ProjectileType<HappyBulletProjectile>());
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<HappyBullet>());
        }
    }
}
