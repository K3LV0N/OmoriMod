using Terraria;
using Terraria.ID;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Melee.Tier1
{
    public class Bat : AngryItem
    {
        public override void SetDefaults()
        {
            SetMeleeWeaponDefaults(
                width: 32,
                height: 32,
                scale: 1.5f,
                buyPrice: Item.buyPrice(0, 1, 0, 0),
                damage: 8,
                knockback: 6,
                useTime: 20,
                useStyleID: ItemUseStyleID.Swing,
                useSound: SoundID.Item1,
                autoReuse: true
            );
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
