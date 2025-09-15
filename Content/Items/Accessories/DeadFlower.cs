using Terraria;
using Terraria.ModLoader;
using OmoriMod.Content.Items.Abstract_Classes;
using OmoriMod.Content.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Buffs.SadBuff;
using OmoriMod.Content.Buffs.Abstract.Helpers;

namespace OmoriMod.Content.Items.Accessories
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
