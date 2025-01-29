using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Weapons.Magic.Tier3;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Magic.Tier4;

namespace OmoriMod.Items.Weapons.Magic.Tier4
{
    public class AngryBombPlusItem : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // damage
            Item.damage = 60;
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
            Item.mana = 48;
            Item.shoot = ModContent.ProjectileType<AngryBombPlus>();

            // price
            Item.value = Item.buyPrice(0, 12, 0, 0);

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AngryBombItem>(), 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.Register();
        }

    }
}
