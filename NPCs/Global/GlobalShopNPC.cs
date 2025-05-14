using OmoriMod.Items.Accessories;
using OmoriMod.Items.BuffItems;
using OmoriMod.Summons.Pets.Items;
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
    }
}
