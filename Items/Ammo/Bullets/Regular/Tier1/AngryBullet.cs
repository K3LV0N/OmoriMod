using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.BuffItems;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Bullets.Regular.Tier1
{
    public class AngryBullet : AngryItem
    {
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1,
                buyPrice: Item.buyPrice(0, 0, 1, 0),
                stackSize: 9999,
                researchCount: 99,
                consumable: true
                );

            ProjectileDefaults(
                ammoID: AmmoID.Bullet,
                projectileID: ModContent.ProjectileType<AngryBulletProjectile>(),
                shootSpeed: 20.5f
                );

            DamageDefaults(
                damageType: DamageClass.Ranged,
                damage: 12,
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

                baseIngredientID: ModContent.ItemType<AirHorn>(),
                baseAmount: 1,

                nonEndlessIngredientID: ItemID.MusketBall,
                nonEndlessAmount: 100,

                endlessIngredientID: ItemID.EndlessMusketPouch
                );
        }
    }
}
