using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier1;
using OmoriMod.Projectiles.Friendly.Melee.Bat;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier2
{
    public class CorruptionBat : AngryItem
    {
        public override void SetDefaults()
        {
            SetMeleeWeaponWithProjectileDefaults<BatProj>(
                width: 32,
                height: 32,
                scale: 1.5f,
                buyPrice: Item.buyPrice(0, 2, 0, 0),
                damage: 20,
                knockback: 6,
                shootSpeed: 8f,
                useTime: 17,
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
            recipe.AddIngredient(ModContent.ItemType<Bat>(), 1);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
