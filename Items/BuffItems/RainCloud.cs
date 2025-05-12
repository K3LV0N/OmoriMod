using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Buffs.HappyBuff;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.BuffItems
{
    public class RainCloud : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentBuff<AirHorn>(ModContent.BuffType<Sad>());
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff<Happy>() || player.HasBuff<HappyNoTime>() || player.HasBuff<Ecstatic>() || player.HasBuff<Manic>() ||
                player.HasBuff<Angry>() || player.HasBuff<AngryNoTime>() || player.HasBuff<Enraged>() || player.HasBuff<Furious>() ||
                player.HasBuff<SadNoTime>())
            {
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {

            if (player.HasBuff<Miserable>())
            {
                player.AddBuff(ModContent.BuffType<Miserable>(), 60 * 60);
            }
            else if (player.HasBuff<Depressed>())
            {
                player.ClearBuff(ModContent.BuffType<Depressed>());
                player.AddBuff(ModContent.BuffType<Miserable>(), 60 * 60);
            }
            else if (player.HasBuff<Sad>() && player.buffTime[player.FindBuffIndex(ModContent.BuffType<Sad>())] != 60 * 60)
            {
                player.ClearBuff(ModContent.BuffType<Sad>());
                player.AddBuff(ModContent.BuffType<Depressed>(), 60 * 60);
            }
            else
            {
                player.AddBuff(ModContent.BuffType<Sad>(), 60 * 60);
            }
            return true;

        }
    }
}
