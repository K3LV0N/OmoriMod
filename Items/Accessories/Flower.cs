using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Accessories
{
    public class Flower : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionalItemClone<BloodyFlower>();
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(ModContent.BuffType<HappyNoTime>(), 2);
        }
    }
}
