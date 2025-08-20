using OmoriMod.Buffs.SadBuff;
using Terraria;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Players;
using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.Buffs.Abstract;
using OmoriMod.Systems.EmotionSystem;

namespace OmoriMod.Items.BuffItems
{
    public class RainCloud : EmotionBuffItem
    {
        RainCloud()
        {
            itemTypeForResearch = ItemTypeForResearch.BuffPotion;
        }
        public override void SetDefaults()
        {
            SetEmotionType(EmotionType.SAD);
            EmotionItemClone<AirHorn>();
        }

        public override bool CanUseItemEmotionBuffItem(Player player)
        {
            EmotionPlayer emotionPlayer = player.GetModPlayer<EmotionPlayer>();

            if (emotionPlayer.Emotion == EmotionType.SAD || emotionPlayer.Emotion == EmotionType.NONE) { return true; }
            return false;
        }

        public override bool? UseItemEmotionBuffItem(Player player)
        {
            EmotionHelper.ApplyOrPromoteBuff<SadEmotionBase>(
                player: player,
                baseBuffType: ModContent.BuffType<Sad>(),
                duration: EmotionHelper.EMOTION_TIME_IN_SECONDS * 60
                );

            return true;
        }
    }
}
