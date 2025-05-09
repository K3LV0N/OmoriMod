﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier2;
using OmoriMod.Items.Weapons.Magic.Tier1;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier2
{
    public class HappyBundle : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBundle>(ModContent.ProjectileType<BundledHappiness>());
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HappyBoltItem>(), 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.Register();
        }

    }
}
