using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier3;
using OmoriMod.Items.Weapons.Magic.Tier2;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier3
{
    public class AngryBombItem : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // damage
            Item.damage = 40;
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
            Item.shootSpeed = 3f;
            Item.mana = 24;
            Item.shoot = ModContent.ProjectileType<AngryBomb>();

            // price
            Item.value = Item.buyPrice(0, 6, 0, 0);

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AngryBundle>(), 1);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.Register();
        }

    }
}
