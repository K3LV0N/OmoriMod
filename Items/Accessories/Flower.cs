using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Buffs.Abstract.Helpers;

namespace OmoriMod.Items.Accessories
{
    public class Flower : HappyItem
    {
        Flower()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemClone<BloodyFlower>();
        }

        public override void UpdateEquip(Player player)
        {
            EmotionHelper.ClearAllEmotions(player);
            player.AddBuff(ModContent.BuffType<HappyNoTime>(), 2);
        }
    }
}
