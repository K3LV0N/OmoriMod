﻿using OmoriMod.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;

namespace OmoriMod.Items.Starter
{
    public class Note : OmoriModItem
    {
        Note()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 32,
                height: 32,
                scale: 1f,
                buyPrice: Item.buyPrice(0, 0, 0, 0),
                stackSize: 1,
                consumable: false
                );

            SetItemRarity(ItemRarityID.Green);
        }

        public override void AddRecipes()
        {
            Recipe test = CreateRecipe();
            test.Register();
        }
    }
}
