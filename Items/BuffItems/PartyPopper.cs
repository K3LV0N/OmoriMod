using OmoriMod.Buffs.HappyBuff;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Players;
using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.Buffs.Abstract;
using OmoriMod.Systems.EmotionSystem;

namespace OmoriMod.Items.BuffItems
{
    public class PartyPopper : EmotionBuffItem
    {
        PartyPopper()
        {
            itemTypeForResearch = ItemTypeForResearch.BuffPotion;
        }
        public override void SetDefaults()
        {
            SetEmotionType(EmotionType.HAPPY);
            EmotionItemClone<AirHorn>();
        }

        public override bool CanUseItemEmotionBuffItem(Player player)
        {
            EmotionPlayer emotionPlayer = player.GetModPlayer<EmotionPlayer>();

            if (emotionPlayer.Emotion == EmotionType.HAPPY || emotionPlayer.Emotion == EmotionType.NONE) { return true; }
            return false;
        }

        public override bool? UseItemEmotionBuffItem(Player player)
        {
            EmotionHelper.ApplyOrPromoteBuff<HappyEmotionBase>(
                player: player,
                baseBuffType: ModContent.BuffType<Happy>(),
                duration: EmotionHelper.emotionTimeInSeconds * 60
                );

            return true;
        }
    }
}
