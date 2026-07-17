using OmoriMod.Content.Buffs.Abstract;
using OmoriMod.Content.Buffs.HappyBuff;
using OmoriMod.Content.Items.Abstract_Classes;
using OmoriMod.Content.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Players;
using OmoriMod.Content.Systems.EmotionSystem;

using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Content.Items.BuffItems;

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

        return emotionPlayer.Emotion == EmotionType.HAPPY || emotionPlayer.Emotion == EmotionType.NONE;
    }

    public override bool? UseItemEmotionBuffItem(Player player)
    {
        EmotionSystem.ApplyOrPromoteEmotion<HappyEmotionBase>(
            player: player,
            duration: EmotionSystem.EMOTION_TIME_IN_SECONDS * 60
        );
        return true;
    }
}