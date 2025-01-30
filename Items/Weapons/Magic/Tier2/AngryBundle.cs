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
            SetMagicWeaponWithProjectileDefaults<BundledAnger>(
                width: 32,
                height: 26,
                buyPrice: Item.buyPrice(0, 3, 0, 0),
                damage: 32,
                knockback: 6,
                shootSpeed: 15f,
                mana: 16,
                useTime: 20,
                useStyleID: ItemUseStyleID.Shoot,
                useSound: SoundID.Item1,
                autoReuse: true
            );
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
