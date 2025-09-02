﻿using OmoriMod.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Projectiles.Friendly.Bullets.Tier2;
using OmoriMod.Content.Items.Ammo.Bullets.Regular.Tier1;
using OmoriMod.Content.Items.Ammo.Bullets.Unlimited.Tier1;

namespace OmoriMod.Content.Items.Ammo.Bullets.Regular.Tier2
{
    public class HappyBulletPlus : HappyItem
    {
        HappyBulletPlus()
        {
            itemTypeForResearch = ItemTypeForResearch.Ammo_Explosives;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBulletPlus>(ModContent.ProjectileType<HappyBulletPlusProjectile>());
            Item.damage = ModContent.GetModItem(ModContent.ItemType<HappyBullet>()).Item.damage;
        }

        public override void AddRecipes()
        {
            // Create recipes
            MakeAmmoRecipes(
                resultAmount: 100,

                baseIngredientID: ItemID.HallowedBar,
                baseAmount: 1,

                nonEndlessIngredientID: ModContent.ItemType<HappyBullet>(),
                nonEndlessAmount: 100,

                endlessIngredientID: ModContent.ItemType<InfiniteHappyBullet>()
                );

        }
    }
}
