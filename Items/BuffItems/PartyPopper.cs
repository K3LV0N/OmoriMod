using OmoriMod.Buffs.HappyBuff;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Players;
using OmoriMod.Systems.EmotionSystem.Interfaces;

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
            EmotionPlayer emotionPlayer = player.GetModPlayer<EmotionPlayer>();

            if (emotionPlayer.Emotion == EmotionType.HAPPY || emotionPlayer.Emotion == EmotionType.NONE) { return true; }
            return false;
        }

        public override bool? UseItem(Player player)
        {
            if (player.HasBuff<Manic>())
            {
                player.AddBuff(ModContent.BuffType<Manic>(), Item.buffTime);
            }
            else if (player.HasBuff<Ecstatic>())
            {
                player.ClearBuff(ModContent.BuffType<Ecstatic>());
                player.AddBuff(ModContent.BuffType<Manic>(), Item.buffTime);
            }
            else if (player.HasBuff<Happy>())
            {
                player.ClearBuff(ModContent.BuffType<Happy>());
                player.AddBuff(ModContent.BuffType<Ecstatic>(), Item.buffTime);
            }
            else
            {
                player.AddBuff(ModContent.BuffType<Happy>(), Item.buffTime);
            }

            return true;
        }
    }
}
