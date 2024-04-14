using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier2;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Bullets.Tier2;

namespace OmoriMod.Items.Ammo.Bullets.Unlimited.Tier2
{
    public class InfiniteAngryBulletPlus : AngryItem
    {
        public override void SetDefaults()
        {
            // consumability and stacks
            Item.consumable = false;
            Item.maxStack = 1;
            Item.ResearchUnlockCount = 3;
            Item.value = Item.buyPrice(0, 15, 0, 0);

            // combat
            Item.damage = 17;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.crit = 4;

            // size
            Item.width = 16;
            Item.height = 16;

            // projectile stuff
            Item.ammo = AmmoID.Bullet;
            Item.shootSpeed = 20.5f;
            Item.shoot = ModContent.ProjectileType<AngryBulletPlusProj>();

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<AngryBulletPlus>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
