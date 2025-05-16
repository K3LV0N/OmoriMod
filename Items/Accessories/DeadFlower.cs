using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Buffs.Abstract.Helpers;

namespace OmoriMod.Items.Accessories
{
    public class DeadFlower : SadItem
    {
        DeadFlower()
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
            player.AddBuff(ModContent.BuffType<SadNoTime>(), 2);
        }
    }
}
