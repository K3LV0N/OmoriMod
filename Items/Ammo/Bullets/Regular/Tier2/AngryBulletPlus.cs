using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier1;
using OmoriMod.Items.Ammo.Bullets.Unlimited.Tier1;
using OmoriMod.Projectiles.Friendly.Bullets.Tier2;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Bullets.Regular.Tier2
{
    public class AngryBulletPlus : AngryItem
    {
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1,
                buyPrice: Item.buyPrice(0, 0, 5, 0),
                stackSize: 9999,
                researchCount: 99,
                consumable: true
                );

            ProjectileDefaults(
                ammoID: AmmoID.Bullet,
                projectileID: ModContent.ProjectileType<AngryBulletPlusProjectile>(),
                shootSpeed: 20.5f
                );

            DamageDefaults(
                damageType: DamageClass.Ranged,
                damage: 17,
                knockback: 1f,
                crit: 4,
                noMelee: true
                );
        }

        public override void AddRecipes()
        {
            // Create recipes
            MakeAmmoRecipes(
                resultAmount: 100,

                baseIngredientID: ItemID.HallowedBar,
                baseAmount: 1,

                nonEndlessIngredientID: ModContent.ItemType<AngryBullet>(),
                nonEndlessAmount: 100,

                endlessIngredientID: ModContent.ItemType<InfiniteAngryBullet>()
                );
        }
    }
}
