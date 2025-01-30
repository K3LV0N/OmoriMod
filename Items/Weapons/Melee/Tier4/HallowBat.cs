using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier3;
using OmoriMod.Projectiles.Friendly.Melee.Bat;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier4
{
    public class HallowBat : AngryItem
    {
        public override void SetDefaults()
        {
            SetMeleeWeaponWithProjectileDefaults<BatProjFive>(
                width: 32,
                height: 32,
                scale: 1.5f,
                buyPrice: Item.buyPrice(0, 8, 0, 0),
                damage: 50,
                knockback: 6,
                shootSpeed: 8f,
                useTime: 14,
                useStyleID: ItemUseStyleID.Swing,
                useSound: SoundID.Item1,
                autoReuse: true
            );
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HellBat>(), 1);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
