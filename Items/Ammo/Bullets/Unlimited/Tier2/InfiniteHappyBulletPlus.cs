using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier2;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Bullets.Tier2;
using OmoriMod.Items.Ammo.Bullets.Unlimited.Tier1;

namespace OmoriMod.Items.Ammo.Bullets.Unlimited.Tier2
{
    public class InfiniteHappyBulletPlus : HappyItem
    {
        InfiniteHappyBulletPlus()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<InfiniteAngryBulletPlus>(ModContent.ProjectileType<HappyBulletPlusProjectile>());
            Item.damage = ModContent.GetModItem(ModContent.ItemType<InfiniteHappyBullet>()).Item.damage;
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<HappyBulletPlus>());
        }
    }
}
