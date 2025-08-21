using OmoriMod.Items.Abstract_Classes;
using System;
using System.Diagnostics;
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

        public override bool CanRightClick()
        {
            return true;
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        private static void OpenUrl(string url)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // important! tells Windows to open the URL in default browser
                };
                Process.Start(psi);
            }
            catch (Exception e)
            {
                Main.NewText("Could not open the link: " + e.Message);
            }
        }

        public override void RightClick(Player player)
        {
            OpenUrl("https://sites.google.com/view/omorimodwiki?usp=sharing");
        }

        public override void AddRecipes()
        {
            Recipe test = CreateRecipe();
            test.Register();
        }
    }
}
