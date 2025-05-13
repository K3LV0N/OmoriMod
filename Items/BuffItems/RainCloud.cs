using OmoriMod.Buffs.SadBuff;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Players;
using OmoriMod.Systems.EmotionSystem.Interfaces;

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
            EmotionPlayer emotionPlayer = player.GetModPlayer<EmotionPlayer>();

            if (emotionPlayer.Emotion == EmotionType.SAD || emotionPlayer.Emotion == EmotionType.NONE) { return true; }
            return false;
        }

        public override bool? UseItem(Player player)
        {

            if (player.HasBuff<Miserable>())
            {
                player.AddBuff(ModContent.BuffType<Miserable>(), Item.buffTime);
            }
            else if (player.HasBuff<Depressed>())
            {
                player.ClearBuff(ModContent.BuffType<Depressed>());
                player.AddBuff(ModContent.BuffType<Miserable>(), Item.buffTime);
            }
            else if (player.HasBuff<Sad>())
            {
                player.ClearBuff(ModContent.BuffType<Sad>());
                player.AddBuff(ModContent.BuffType<Depressed>(), Item.buffTime);
            }
            else
            {
                player.AddBuff(ModContent.BuffType<Sad>(), Item.buffTime);
            }
            return true;

        }
    }
}
