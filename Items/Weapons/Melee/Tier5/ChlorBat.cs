using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier4;
using OmoriMod.Projectiles.Friendly.Melee.Bat;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier5
{
    public class ChlorBat : AngryItem
    {
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 32,
                height: 32,
                scale: 1.5f,
                buyPrice: Item.buyPrice(0, 16, 0, 0),
                stackSize: 1,
                researchCount: 1,
                consumable: false
                );

            DamageDefaults(
                damageType: DamageClass.Melee,
                damage: 70,
                knockback: 6f,
                crit: 4,
                noMelee: false
                );

            ProjectileDefaults(
                ammoID: AmmoID.None,
                projectileID: ModContent.ProjectileType<BatProjectileFiveSeeking>(),
                shootSpeed: 8f
                );

            AnimationDefaults(
                useTime: 12,
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
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<HallowBat>(),
                extraItemID: ItemID.ChlorophyteBar,
                extraItemAmount: 25,
                craftingStationID: TileID.MythrilAnvil
                );
        }
    }
}
