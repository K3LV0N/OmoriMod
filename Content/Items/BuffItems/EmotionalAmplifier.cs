using OmoriMod.Content.Buffs.Abstract;
using OmoriMod.Content.Items.Abstract_Classes;
using OmoriMod.Content.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Systems.EmotionSystem;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Content.Items.BuffItems;

public class EmotionalAmplifier : EmotionBuffItem
{
    EmotionalAmplifier()
    {
        cooldownTicks = 5;
        itemTypeForResearch = ItemTypeForResearch.BuffPotion;
    }
    public override void SetDefaults()
    {
        EmotionItemClone<AirHorn>();
        SetItemRarity(ItemRarityID.Purple);
    }

    public override bool CanUseItemEmotionBuffItem(Player player)
    {
        int? buffType = EmotionSystem.GetEmotionType(player);
        if (!buffType.HasValue) return false;

        if (ModContent.GetModBuff(buffType.Value) is not EmotionBuff emotionBuff)
        {
            return false;
        }

        int? currentTier = EmotionSystem.GetEmotionTier(buffType.Value);
        int? maxTier = EmotionSystem.GetMaxEmotionTier(emotionBuff.Emotion);
        return currentTier.HasValue
            && maxTier.HasValue
            && currentTier.Value >= maxTier.Value - 1;
    }

    public override bool? UseItemEmotionBuffItem(Player player)
    {
        int? buffType = EmotionSystem.GetEmotionType(player);
        ModBuff buff = ModContent.GetModBuff(buffType.Value);

        if (buff is AngryEmotionBase)
        {
            EmotionSystem.ApplyOrPromoteEmotion<AngryEmotionBase>(
                player: player,
                duration: EmotionSystem.EMOTION_TIME_IN_SECONDS * 60,
                canPromoteToFinalTier: true
            );
        }
        if (buff is HappyEmotionBase)
        {
            EmotionSystem.ApplyOrPromoteEmotion<HappyEmotionBase>(
                player: player,
                duration: EmotionSystem.EMOTION_TIME_IN_SECONDS * 60,
                canPromoteToFinalTier: true
            );
        }
        if (buff is SadEmotionBase)
        {
            EmotionSystem.ApplyOrPromoteEmotion<SadEmotionBase>(
                player: player,
                duration: EmotionSystem.EMOTION_TIME_IN_SECONDS * 60,
                canPromoteToFinalTier: true
            );
        }

        return true;
    }
}
