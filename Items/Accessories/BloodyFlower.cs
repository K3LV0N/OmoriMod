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
            int value = Item.buyPrice(1, 0, 0, 0);
            SetAccessoryDefaults(width: 20, height: 30, buyPrice: value);
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(ModContent.BuffType<AngryNoTime>(), 2);
        }
    }
}
