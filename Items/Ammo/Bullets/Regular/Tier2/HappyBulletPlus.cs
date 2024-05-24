using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier1;
using OmoriMod.Items.Ammo.Bullets.Unlimited.Tier1;
using OmoriMod.Projectiles.Friendly.Bullets.Tier2;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Bullets.Regular.Tier2
{
    public class HappyBulletPlus : HappyItem
    {

        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 99;
            // clone default bullet stuff
            Item.CloneDefaults(ModContent.ItemType<AngryBullet>());

            // damage stuff
            Item.damage = ModContent.GetModItem(ModContent.ItemType<HappyBullet>()).Item.damage;

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<HappyBulletPlusProj>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe(100);
            recipe1.AddIngredient(ModContent.ItemType<HappyBullet>(), 100);
            recipe1.AddIngredient(ItemID.HallowedBar, 1);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe(100);
            recipe2.AddIngredient(ItemID.HallowedBar, 1);
            recipe2.AddCondition(Condition.PlayerCarriesItem(ModContent.ItemType<InfiniteHappyBullet>()));
            recipe2.Register();

        }
    }
}
