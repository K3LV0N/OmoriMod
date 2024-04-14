using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Accessories
{
    public class BloodyFlower : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // size
            Item.width = 20;
            Item.height = 30;

            // accessory
            Item.accessory = true;

            // price
            Item.value = Item.buyPrice(1, 0, 0, 0);
            
            // angry item
            SetAngryDefaults();
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(ModContent.BuffType<AngryNoTime>(), 2);
        }
    }
}
