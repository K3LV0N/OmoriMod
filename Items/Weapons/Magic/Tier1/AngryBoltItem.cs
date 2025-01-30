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
            SetMagicWeaponWithProjectileDefaults<AngryBolt>(
                width: 32,
                height: 26,
                buyPrice: Item.buyPrice(0, 1, 50, 0),
                damage: 18,
                knockback: 6,
                shootSpeed: 15f,
                mana: 8,
                useTime: 20,
                useStyleID: ItemUseStyleID.Shoot,
                useSound: SoundID.Item1,
                autoReuse: true
            );
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
