using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier3;
using OmoriMod.Items.Weapons.Magic.Tier2;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier3
{
    public class AngryBomb : AngryItem
    {
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 32,
                height: 26,
                scale: 1f,
                buyPrice: Item.buyPrice(0, 6, 0, 0),
                stackSize: 1,
                researchCount: 1,
                consumable: false
                );

            DamageDefaults(
                damageType: DamageClass.Magic,
                damage: 40,
                knockback: 6f,
                crit: 4,
                noMelee: true,
                mana: 24
                );

            ProjectileDefaults(
                ammoID: AmmoID.None,
                projectileID: ModContent.ProjectileType<AngryBombProjectile>(),
                shootSpeed: 3f
                );

            AnimationDefaults(
                useTime: 20,
                useStyleID: ItemUseStyleID.Shoot,
                useSound: SoundID.Item1,
                autoReuse: true
                );
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<AngryBundle>(),
                extraItemID: ItemID.HallowedBar,
                extraItemAmount: 20,
                craftingStationID: TileID.Bookcases
                );
        }
    }
}
