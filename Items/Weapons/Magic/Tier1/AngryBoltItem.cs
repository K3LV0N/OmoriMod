using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier1;
using OmoriMod.Items.BuffItems;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier1
{
    public class AngryBoltItem : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // damage
            Item.damage = 18;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;

            // size
            Item.height = 32;
            Item.width = 26;

            // usage
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            // projectiles
            Item.shootSpeed = 15f;
            Item.mana = 8;
            Item.shoot = ModContent.ProjectileType<AngryBolt>();

            // price
            Item.value = Item.buyPrice(0, 1, 50, 0);

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient<AirHorn>(10);
            recipe.Register();
        }

    }
}
