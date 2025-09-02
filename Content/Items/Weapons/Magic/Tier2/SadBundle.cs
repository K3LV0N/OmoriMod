﻿using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Items.Weapons.Magic.Tier1;
using OmoriMod.Content.Projectiles.Friendly.Magic.Tier2;

namespace OmoriMod.Content.Items.Weapons.Magic.Tier2
{
    public class SadBundle : SadItem
    {
        SadBundle()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBundle>(ModContent.ProjectileType<SadBundleProjectile>());
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<SadBolt>(),
                extraItemID: ItemID.HellstoneBar,
                extraItemAmount: 15,
                craftingStationID: TileID.Bookcases
                );
        }
    }
}
