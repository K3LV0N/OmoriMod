﻿using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Unlimited.Tier1
{
    public class InfiniteHappyArrow : HappyItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 3;
            // clone default arrow stuff
            Item.CloneDefaults(ModContent.ItemType<InfiniteAngryArrow>());

            // projectile stuff
            Item.shoot = ModContent.ProjectileType<HappyArrowProjectileNoDrop>();

            // happy item
            SetHappyDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<HappyArrow>(), 3996);
            recipe1.AddTile(TileID.CrystalBall);
            recipe1.Register();
        }
    }
}
