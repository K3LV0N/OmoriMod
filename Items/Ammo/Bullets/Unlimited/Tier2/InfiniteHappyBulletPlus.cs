using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier2;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Bullets.Tier2;
using OmoriMod.Items.Ammo.Bullets.Unlimited.Tier1;

namespace OmoriMod.Items.Ammo.Bullets.Unlimited.Tier2
{
    public class InfiniteHappyBulletPlus : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 3;
            // clone default bullet stuff
            Item.CloneDefaults(ModContent.ItemType<InfiniteAngryBulletPlus>());

            // damage stuff
            Item.damage = ModContent.GetModItem(ModContent.ItemType<InfiniteHappyBullet>()).Item.damage;

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<HappyBulletPlusProjectile>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<HappyBulletPlus>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
