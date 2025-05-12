using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.NoDrops;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier2
{
    public class InfiniteAngryArrowPlus : AngryItem
    {
        public override void SetDefaults()
        {


            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1,
                buyPrice: Item.buyPrice(0, 5, 0, 0),
                stackSize: 1,
                researchCount: 3,
                consumable: false
                );

            ProjectileDefaults(
                ammoID: AmmoID.Arrow,
                projectileID: ModContent.ProjectileType<AngryArrowPlusProjectileNoDrop>(),
                shootSpeed: 8.5f
                );

            DamageDefaults(
                damageType: DamageClass.Ranged,
                damage: 24,
                knockback: 1f,
                crit: 4,
                noMelee: true
                );
        }

        public override void AddRecipes()
        {
            MakeEndlessAmmoRecipe(ModContent.ItemType<AngryArrowPlus>());
        }
    }
}
