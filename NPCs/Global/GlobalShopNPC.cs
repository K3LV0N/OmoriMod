using OmoriMod.Items.Accessories;
using OmoriMod.Items.BuffItems;
using OmoriMod.Summons.Pets.Items;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.NPCs.Global
{
    public class GlobalShopNPC : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add<PartyPopper>();
                shop.Add<RainCloud>();
                shop.Add<AirHorn>();
                shop.Add<Something>();
            }

            if (shop.NpcType == NPCID.Dryad)
            {
                shop.Add<Flower>();
                shop.Add<DeadFlower>();
                shop.Add<BloodyFlower>();
            }
        }

        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (npc.type == NPCID.Merchant && Main.hardMode)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].type == ItemID.None)
                    {
                        items[i].SetDefaults(ModContent.ItemType<EmotionalAmplifier>());
                        break;
                    }
                }
            }
        }
    }
}
