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
            SetMagicWeaponWithProjectileDefaults<AngryBomb>(
                width: 32,
                height: 26,
                buyPrice: Item.buyPrice(0, 6, 0, 0),
                damage: 40,
                knockback: 6,
                shootSpeed: 3f,
                mana: 24,
                useTime: 20,
                useStyleID: ItemUseStyleID.Shoot,
                useSound: SoundID.Item1,
                autoReuse: true
            );
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
