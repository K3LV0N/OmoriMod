using Terraria;
using Terraria.ModLoader;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Accessories
{
    public class DeadFlower : SadItem
    {
        public override void SetDefaults()
        {
            EmotionalItemClone<BloodyFlower>();
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(ModContent.BuffType<SadNoTime>(), 2);
        }
    }
}
