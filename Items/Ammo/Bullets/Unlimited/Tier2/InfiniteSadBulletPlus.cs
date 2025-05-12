using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier2;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Bullets.Tier2;

namespace OmoriMod.Items.Ammo.Bullets.Unlimited.Tier2
{
    public class InfiniteSadBulletPlus : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<InfiniteAngryBulletPlus>(ModContent.ProjectileType<SadBulletPlusProjectile>());
            Item.damage = 50;
            Item.shootSpeed = 30f;
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<SadBulletPlus>());
        }
    }
}
