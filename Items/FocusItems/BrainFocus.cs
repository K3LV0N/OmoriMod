using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.DamageClasses;
using OmoriMod.Projectiles.Friendly.FocusProjectiles;
using OmoriMod.Items.BuffItems;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.FocusItems
{
    public class BrainFocus : FocusItem
    {
        public override void SetDefaults()
        { 
            InitFocusItem(
                maxCharge: 7,
                dpsIncrease: 2,
                ticksUntilChargeStarts: 40,
                ticksUntilDecayStarts: 20,
                tickDecayRate: 2
                );

            ItemDefaults(
                width: 26,
                height: 32,
                scale: 1f,
                buyPrice: Item.buyPrice(platinum: 0, gold: 1, silver: 0, copper: 0),
                stackSize: 1,
                researchCount: 1,
                consumable: false
                );

            DamageDefaults(
                damageType: ModContent.GetInstance<FocusDamage>(),
                damage: 3,
                knockback: 6f,
                crit: 4,
                noMelee: true
                );

            ProjectileDefaults(
                ammoID: AmmoID.None,
                projectileID: ModContent.ProjectileType<BrainBolt>(),
                shootSpeed: 15f
                );

            AnimationDefaults(
                useTime: 7,
                useStyleID: ItemUseStyleID.Shoot,
                useSound: SoundID.Item1,
                autoReuse: true,
                noUseAnimation: true
                );

            SetItemRarity(ItemRarityID.Blue);
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ItemID.Book,
                extraItemID: ModContent.ItemType<RainCloud>(),
                extraItemAmount: 10,
                craftingStationID: TileID.Bookcases
                );
        }
    }
}
