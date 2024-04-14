using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.BuffItems;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Bullets.Regular.Tier1
{
    public class AngryBullet : AngryItem
    {
        public override void SetDefaults()
        {
            // consumability and stacks
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.ResearchUnlockCount = 99;
            Item.value = Item.buyPrice(0, 0, 1, 0);

            // combat
            Item.damage = 12;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.crit = 4;

            // size
            Item.width = 16;
            Item.height = 16;

            // projectile stuff
            Item.ammo = AmmoID.Bullet;
            Item.shootSpeed = 20.5f;
            Item.shoot = ModContent.ProjectileType<AngryBulletProj>();

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe(100);
            recipe1.AddIngredient(ModContent.ItemType<AirHorn>(), 1);
            recipe1.AddIngredient(ItemID.MusketBall, 100);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe(100);
            recipe2.AddIngredient(ModContent.ItemType<AirHorn>(), 1);
            recipe2.AddCondition(Condition.PlayerCarriesItem(ItemID.EndlessMusketPouch));
            recipe2.Register();
        }
    }
}
