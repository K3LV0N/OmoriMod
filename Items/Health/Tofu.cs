using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Health
{
    public class Tofu : ModItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 50;

            // potion things
            Item.DefaultToHealingPotion(32, 32, 5);

            // consumability and stacks
            Item.consumable = true;
            Item.maxStack = 999;

            // size
            Item.width = 16;
            Item.height = 16;

            // usage
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;

            // rarity
            Item.rare = ItemRarityID.Green;

            // price
            Item.value = Item.buyPrice(platinum: 0, gold: 0, silver: 0, copper: 2);


        }
    }
}
