using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier1;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Bullets.Unlimited.Tier1
{
    public class InfiniteSadBullet : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<InfiniteAngryBullet>(ModContent.ProjectileType<SadBulletProjectile>());
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<SadBullet>());
        }
    }
}
