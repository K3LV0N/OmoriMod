using Microsoft.Xna.Framework;
using OmoriMod.Content.Items.Weapons.Melee.Tier1;
using OmoriMod.Content.Projectiles.Friendly.Melee.Bat;
using OmoriMod.Content.Items.Abstract_Classes;
using OmoriMod.Content.Items.Abstract_Classes.BaseClasses;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Content.Items.Weapons.Melee.Tier2
{
    public class CorruptionBat : AngryItem
    {
        CorruptionBat()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 32,
                height: 32,
                scale: 1.5f,
                buyPrice: Item.buyPrice(0, 2, 0, 0),
                stackSize: 1,
                consumable: false
                );

            DamageDefaults(
                damageType: DamageClass.Melee,
                damage: 20,
                knockback: 6f,
                crit: 4,
                noMelee: false
                );

            ProjectileDefaults(
                ammoID: AmmoID.None,
                projectileID: ModContent.ProjectileType<BatProjectile>(),
                shootSpeed: 8f
                );

            AnimationDefaults(
                useTime: 17,
                useStyleID: ItemUseStyleID.Swing,
                useSound: SoundID.Item1,
                autoReuse: true
                );
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, ref type, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<Bat>(),
                extraItemID: ItemID.DemoniteBar,
                extraItemAmount: 10,
                craftingStationID: TileID.Anvils
                );
        }
    }
}
