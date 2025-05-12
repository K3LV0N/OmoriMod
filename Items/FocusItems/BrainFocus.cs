using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.DamageClasses;
using OmoriMod.Projectiles.Friendly.FocusProjectiles;
using OmoriMod.Items.BuffItems;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.FocusItems
{
    public class BrainFocus : FocusClass
    {
        public override void SetDefaults()
        { 
            // focus class fields
            charge = 0;
            dps = 2;
            maxCharge = 7 * 60;
            timer = 0;
            chargeTimer = 0;
            decayTimer = 0;
            timeUntilChargeStarts = 40;
            timeUntilDecayStarts = 20;
            decayRate = 2;
            charging = false;
            decaying = true;

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

        public override void HoldItem(Player player)
        {
            HoldItemFocus(player);
        }

        public override void UpdateInventory(Player player)
        {
            UpdateInventoryFocus(player);
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
