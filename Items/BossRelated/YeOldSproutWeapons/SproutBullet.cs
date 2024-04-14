using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using OmoriMod.Items.Health;
using OmoriMod.Projectiles.Friendly.BossRelated.YeOldSprout;

namespace OmoriMod.Items.BossRelated.YeOldSproutWeapons
{
    public class SproutBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 99;

            // consumability and stack
            Item.consumable = true;
            Item.maxStack = 9999;

            // damage
            Item.damage = 6;
            Item.knockBack = 1;
            Item.crit = 4;
            
            // size
            Item.width = 16;
            Item.height = 16;

            // usage
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;

            // projectiles
            Item.ammo = Item.type;
            Item.shootSpeed = 20.5f;
            Item.shoot = ModContent.ProjectileType<SproutBulletProj>();

            // rarity
            Item.rare = ItemRarityID.Purple;

            // price
            Item.value = Item.buyPrice(platinum: 0, gold: 0, silver: 0, copper: 3);

        }

        public override void AddRecipes()
        {
            Recipe r1 = CreateRecipe();
            r1.AddIngredient<Tofu>(1);
            r1.ReplaceResult(this, 25);
            r1.Register();
        }
    }
}
