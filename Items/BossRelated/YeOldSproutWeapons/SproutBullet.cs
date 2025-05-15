﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using OmoriMod.Items.Health;
using OmoriMod.Projectiles.Friendly.BossRelated.YeOldSprout;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.BossRelated.YeOldSproutWeapons
{
    public class SproutBullet : OmoriModItem
    {
        SproutBullet()
        {
            itemTypeForResearch = ItemTypeForResearch.Ammo_Explosives;
        }
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1f,
                buyPrice: Item.buyPrice(platinum: 0, gold: 0, silver: 0, copper: 3),
                stackSize: 9999,
                consumable: true
                );

            DamageDefaults(
                damageType: DamageClass.Ranged,
                damage: 6,
                knockback: 1f,
                crit: 4,
                noMelee: true
                );

            ProjectileDefaults(
                ammoID: Item.type,
                projectileID: ModContent.ProjectileType<SproutBulletProjectile>(),
                shootSpeed: 20.5f
                );

            SetItemRarity(ItemRarityID.Purple);
        }

        public override void AddRecipes()
        {
            Recipe r1 = CreateRecipe(25);
            r1.AddIngredient<Tofu>(1);
            r1.Register();
        }
    }
}
