using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Buffs.Abstract.Helpers;

namespace OmoriMod.Items.Accessories
{
    public class BloodyFlower : AngryItem
    {
        BloodyFlower()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            SetAccessoryDefaults(
                width: 20, 
                height: 30, 
                buyPrice: Item.buyPrice(1, 0, 0, 0)
                );
        }

        public override void UpdateEquip(Player player)
        {
            EmotionHelper.ClearAllEmotions(player);
            player.AddBuff(ModContent.BuffType<AngryNoTime>(), 2);
        }
    }
}
