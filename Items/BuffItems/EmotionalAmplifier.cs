using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.Buffs.Abstract;
using OmoriMod.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.AngryBuff;

namespace OmoriMod.Items.BuffItems
{
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
            int? buffType = EmotionHelper.GetEmotionType(player);
            if (!buffType.HasValue) return false;
            if (EmotionHelper.TIER3_EMOTION_TYPES.Contains(buffType.Value) || EmotionHelper.TIER4_EMOTION_TYPES.Contains(buffType.Value)) return true;
            return false;
        }

        public override bool? UseItemEmotionBuffItem(Player player)
        {
            int? buffType = EmotionHelper.GetEmotionType(player);
            ModBuff buff = ModContent.GetModBuff(buffType.Value);
            if(buff is AngryEmotionBase)
            {
                EmotionHelper.ApplyTier4Emotion<AngryEmotionBase>(
                    player: player,
                    baseBuffType: ModContent.BuffType<Livid>(),
                    duration: EmotionHelper.EMOTION_TIME_IN_SECONDS * 60
                );
            }
            if (buff is HappyEmotionBase)
            {
                EmotionHelper.ApplyTier4Emotion<HappyEmotionBase>(
                    player: player,
                    baseBuffType: ModContent.BuffType<Hysterical>(),
                    duration: EmotionHelper.EMOTION_TIME_IN_SECONDS * 60
                );
            }
            if (buff is SadEmotionBase)
            {
                EmotionHelper.ApplyTier4Emotion<SadEmotionBase>(
                    player: player,
                    baseBuffType: ModContent.BuffType<Despondent>(),
                    duration: EmotionHelper.EMOTION_TIME_IN_SECONDS * 60
                );
            }

            return true;
        }
    }
}
