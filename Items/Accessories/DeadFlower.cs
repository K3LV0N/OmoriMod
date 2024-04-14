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
            Item.ResearchUnlockCount = 1;

            // clone default accessory stuff
            Item.CloneDefaults(ModContent.ItemType<BloodyFlower>());

            // sad item
            SetSadDefaults();
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(ModContent.BuffType<SadNoTime>(), 2);
        }
    }
}
