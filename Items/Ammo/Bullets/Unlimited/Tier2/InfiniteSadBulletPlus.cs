using Terraria;
using Terraria.ID;
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
            Item.ResearchUnlockCount = 3;
            // clone default bullet stuff
            Item.CloneDefaults(ModContent.ItemType<InfiniteAngryBulletPlus>());

            // changes
            Item.damage = 50;
            Item.shootSpeed = 30f;

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<SadBulletPlusProjectile>();

            // sad item
            SetSadDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<SadBulletPlus>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
