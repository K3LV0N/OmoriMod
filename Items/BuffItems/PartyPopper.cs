using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Buffs.HappyBuff;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.BuffItems
{
    public class PartyPopper : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentBuff<AirHorn>(ModContent.BuffType<Happy>());
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff<Sad>() || player.HasBuff<SadNoTime>() || player.HasBuff<Depressed>() || player.HasBuff<Miserable>() ||
                player.HasBuff<Angry>() || player.HasBuff<AngryNoTime>() || player.HasBuff<Enraged>() || player.HasBuff<Furious>() ||
                player.HasBuff<HappyNoTime>())
            {
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.HasBuff<Manic>())
            {
                player.AddBuff(ModContent.BuffType<Manic>(), 60 * 60);
            }
            else if (player.HasBuff<Ecstatic>())
            {
                player.ClearBuff(ModContent.BuffType<Ecstatic>());
                player.AddBuff(ModContent.BuffType<Manic>(), 60 * 60);
            }
            else if (player.HasBuff<Happy>() && player.buffTime[player.FindBuffIndex(ModContent.BuffType<Happy>())] != 60 * 60)
            {
                player.ClearBuff(ModContent.BuffType<Happy>());
                player.AddBuff(ModContent.BuffType<Ecstatic>(), 60 * 60);
            }
            else
            {
                player.AddBuff(ModContent.BuffType<Happy>(), 60 * 60);
            }

            return true;
        }
    }
}
