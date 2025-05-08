using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.BuffItems;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Bullets.Regular.Tier1
{
    public class HappyBullet : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 99;
            // clone default bullet stuff
            Item.CloneDefaults(ModContent.ItemType<AngryBullet>());

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<HappyBulletProjectile>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe(100);
            recipe1.AddIngredient(ModContent.ItemType<PartyPopper>(), 1);
            recipe1.AddIngredient(ItemID.MusketBall, 100);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe(100);
            recipe2.AddIngredient(ModContent.ItemType<PartyPopper>(), 1);
            recipe2.AddCondition(Condition.PlayerCarriesItem(ItemID.EndlessMusketPouch));
            recipe2.Register();

        }
    }
}
