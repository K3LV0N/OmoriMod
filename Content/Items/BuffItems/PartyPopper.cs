using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Systems.EmotionSystem;
using OmoriMod.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Players;
using OmoriMod.Content.Buffs.Abstract;
using OmoriMod.Content.Buffs.HappyBuff;
using OmoriMod.Content.Buffs.Abstract.Helpers;

namespace OmoriMod.Content.Items.BuffItems
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
                duration: EmotionHelper.EMOTION_TIME_IN_SECONDS * 60
                );

            return true;
        }
    }
}
