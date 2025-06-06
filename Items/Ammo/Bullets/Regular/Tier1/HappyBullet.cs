﻿using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.BuffItems;
using OmoriMod.Projectiles.Friendly.Bullets.Tier1;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Bullets.Regular.Tier1
{
    public class HappyBullet : HappyItem
    {
        HappyBullet()
        {
            itemTypeForResearch = ItemTypeForResearch.Ammo_Explosives;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBullet>(ModContent.ProjectileType<HappyBulletProjectile>());
        }

        public override void AddRecipes()
        {
            // Create recipes
            MakeAmmoRecipes(
                resultAmount: 100,

                baseIngredientID: ModContent.ItemType<PartyPopper>(),
                baseAmount: 1,

                nonEndlessIngredientID: ItemID.MusketBall,
                nonEndlessAmount: 100,

                endlessIngredientID: ItemID.EndlessMusketPouch
                );
        }
    }
}
