﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Unlimited.Tier1;
using OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop;

namespace OmoriMod.Items.Ammo.Arrows.Regular.Tier2
{
    public class HappyArrowPlus : HappyItem
    {
        HappyArrowPlus()
        {
            itemTypeForResearch = ItemTypeForResearch.Ammo_Explosives;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryArrowPlus>(ModContent.ProjectileType<HappyArrowPlusProjectile>());
            Item.damage = ModContent.GetModItem(ModContent.ItemType<HappyArrow>()).Item.damage;
        }

        public override void AddRecipes()
        {
            // Create recipes
            MakeAmmoRecipes(
                resultAmount: 100,

                baseIngredientID: ItemID.HallowedBar,
                baseAmount: 1,

                nonEndlessIngredientID: ModContent.ItemType<HappyArrow>(),
                nonEndlessAmount: 100,

                endlessIngredientID: ModContent.ItemType<InfiniteHappyArrow>()
                );
        }
    }
}
