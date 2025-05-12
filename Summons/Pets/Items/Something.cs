using Microsoft.Xna.Framework;
using OmoriMod.Summons.Pets.Buffs;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Summons.Abstract_Classes;

namespace OmoriMod.Summons.Pets.Items
{
    public class Something : ModPetItem
    {
        public override void SetDefaults()
        {
            SetPetDefaults();

            // The projectile will come from the buff
            Item.buffType = ModContent.BuffType<SomethingBuff>();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 69);
            }
        }
    }
}
