using Terraria;
using Terraria.ModLoader;
using OmoriMod.Content.Summons.Abstract_Classes;
using OmoriMod.Content.Summons.Pets.Buffs;

namespace OmoriMod.Content.Summons.Pets.Items
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