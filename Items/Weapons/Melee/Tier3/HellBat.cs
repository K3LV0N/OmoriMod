using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier2;
using OmoriMod.Projectiles.Friendly.Melee.Bat;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier3
{
    public class HellBat : AngryItem
    {
        public override void SetDefaults()
        {
            SetMeleeWeaponWithProjectileDefaults<BatProjectileTriple>(
                width: 32,
                height: 32,
                scale: 1.5f,
                buyPrice: Item.buyPrice(0, 4, 0, 0),
                damage: 35,
                knockback: 6,
                shootSpeed: 8f,
                useTime: 15,
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
            recipe.AddIngredient(ModContent.ItemType<CorruptionBat>(), 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<CrimsonBat>(), 1);
            recipe2.AddIngredient(ItemID.HellstoneBar, 15);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}
