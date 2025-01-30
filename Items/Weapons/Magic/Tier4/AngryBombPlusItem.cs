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
            SetMagicWeaponWithProjectileDefaults<AngryBombPlus>(
                width: 32,
                height: 26,
                buyPrice: Item.buyPrice(0, 12, 0, 0),
                damage: 60,
                knockback: 6,
                shootSpeed: 3f,
                mana: 48,
                useTime: 20,
                useStyleID: ItemUseStyleID.Shoot,
                useSound: SoundID.Item1,
                autoReuse: true
            );
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
