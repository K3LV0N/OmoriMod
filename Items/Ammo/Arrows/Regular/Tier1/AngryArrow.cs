﻿using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.BuffItems;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.CanDrop;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Regular.Tier1
{
    public class AngryArrow : AngryItem
    {
        AngryArrow()
        {
            itemTypeForResearch = ItemTypeForResearch.Ammo_Explosives;
        }
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1,
                buyPrice: Item.buyPrice(0, 0, 1, 0),
                stackSize: 9999,
                consumable: true
                );

            ProjectileDefaults(
                ammoID: AmmoID.Arrow,
                projectileID: ModContent.ProjectileType<AngryArrowProjectile>(),
                shootSpeed: 8.5f
                );

            DamageDefaults(
                damageType: DamageClass.Ranged,
                damage: 14,
                knockback: 1f,
                crit: 4,
                noMelee: true
                );
        }

        public override void AddRecipes()
        {
            // Create recipes
            MakeAmmoRecipes(
                resultAmount: 50,

                baseIngredientID: ModContent.ItemType<AirHorn>(),
                baseAmount: 1,

                nonEndlessIngredientID: ItemID.WoodenArrow,
                nonEndlessAmount: 50,

                endlessIngredientID: ItemID.EndlessQuiver       
                );
        }
    }
}
