using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier1;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Bullets.Unlimited.Tier1
{
    public class InfiniteHappyBullet : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 3;
            // clone default bullet stuff
            Item.CloneDefaults(ModContent.ItemType<InfiniteAngryBullet>());

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<HappyBulletProj>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<HappyBullet>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
