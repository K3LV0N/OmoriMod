using Microsoft.Xna.Framework;
using OmoriMod.Buffs.Pets;
using OmoriMod.Projectiles.Pets;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Pets
{
    public class SomethingItem : ModItem
    {

        // props to Lynx on youtube for providing the following code
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // clone regular pet things
            Item.CloneDefaults(ItemID.DD2PetGato);

            // change to our pet
            Item.shoot = ModContent.ProjectileType<SomethingProj>();
            Item.buffType = ModContent.BuffType<SomethingBuff>();
            Item.value = Item.buyPrice(platinum:1, gold:0, silver:0, copper:0);
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if(player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffTime, 3600);
            }
        }
    }
}
