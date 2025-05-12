using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Ammo.Bullets.Regular.Tier2;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Bullets.Tier2;

namespace OmoriMod.Items.Ammo.Bullets.Unlimited.Tier2
{
    public class InfiniteAngryBulletPlus : AngryItem
    {
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1,
                buyPrice: Item.buyPrice(0, 15, 0, 0),
                stackSize: 1,
                researchCount: 3,
                consumable: false
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
            MakeEndlessAmmoRecipe(ModContent.ItemType<AngryBulletPlus>());
        }
    }
}
