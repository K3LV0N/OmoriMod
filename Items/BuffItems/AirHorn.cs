using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.BuffItems
{
    public class AirHorn : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 50;

            // stacks and consumability
            Item.consumable = true;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0,0,2,0);

            // size
            Item.width = 16;
            Item.height = 16;

            // usage
            Item.autoReuse = false;
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.HoldUp;
                   
            // applied buff
            Item.buffType = ModContent.BuffType<Angry>();

            // angry item
            SetAngryDefaults();

        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff<Sad>() || player.HasBuff<SadNoTime>() || player.HasBuff<Depressed>() || player.HasBuff<Miserable>() ||
                player.HasBuff<Happy>() || player.HasBuff<HappyNoTime>() || player.HasBuff<Ecstatic>() || player.HasBuff<Manic>() ||
                player.HasBuff<AngryNoTime>())
            {
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.HasBuff<Furious>())
            {
                player.AddBuff(ModContent.BuffType<Furious>(), 60 * 60);
            }
            else if (player.HasBuff<Enraged>())
            {
                player.ClearBuff(ModContent.BuffType<Enraged>());
                player.AddBuff(ModContent.BuffType<Furious>(), 60 * 60);
            }
            else if (player.HasBuff<Angry>() && player.buffTime[player.FindBuffIndex(ModContent.BuffType<Angry>())] != 60 * 60)
            {
                player.ClearBuff(ModContent.BuffType<Angry>());
                player.AddBuff(ModContent.BuffType<Enraged>(), 60 * 60);
            }
            else
            {
                player.AddBuff(ModContent.BuffType<Angry>(), 60 * 60);
            }

            return true;
        }
    }
}
