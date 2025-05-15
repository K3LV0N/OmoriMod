using OmoriMod.Summons.Pets.Buffs;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Summons.Abstract_Classes;

namespace OmoriMod.Summons.Pets.Items
{
    public class Something : ModPetItem
    {
        public override void PetSetDefaults()
        {
            // The projectile will come from the buff
            Item.buffType = ModContent.BuffType<SomethingBuff>();
        }
    }
}