using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier2;
using OmoriMod.Items.Weapons.Magic.Tier1;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier2
{
    public class AngryBundle : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // damage
            Item.damage = 32;
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
            Item.mana = 16;
            Item.shoot = ModContent.ProjectileType<BundledAnger>();

            // price
            Item.value = Item.buyPrice(0, 3, 0, 0);

            // angry item
            SetAngryDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AngryBoltItem>(), 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.Register();
        }

    }
}
