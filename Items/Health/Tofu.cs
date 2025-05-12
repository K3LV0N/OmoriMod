using OmoriMod.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;

namespace OmoriMod.Items.Health
{
    public class Tofu : EmotionItem
    {
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1f,
                buyPrice: Item.buyPrice(platinum: 0, gold: 0, silver: 0, copper: 2),
                stackSize: 999,
                researchCount: 50,
                consumable: true
                );

            AnimationDefaults(
                useTime: 17,
                useStyleID: ItemUseStyleID.DrinkLiquid,
                useSound: SoundID.Item3,
                autoReuse: false,
                canTurnWhileUsing: true
                );

            PotionDefaults(
                healthHealed: 5,
                manaHealed: 0,
                isPotion: true
                );

            SetItemRarity(ItemRarityID.Blue);
        }
    }
}
