using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Players;
using OmoriMod.Systems.EmotionSystem.Interfaces;
using OmoriMod.Buffs.Abstract;
using OmoriMod.Buffs.Abstract.Helpers;

namespace OmoriMod.Items.BuffItems
{
    public class AirHorn : EmotionBuffItem
    {
        public override void SetDefaults()
        {
            SetEmotionType(EmotionType.ANGRY);

            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1f,
                buyPrice: Item.buyPrice(0, 0, 2, 0),
                stackSize: 999,
                researchCount: 50,
                consumable: true
                );

            AnimationDefaults(
                useTime: 20,
                useStyleID: ItemUseStyleID.HoldUp,
                useSound: SoundID.Item1,
                autoReuse: false
                );

            PotionDefaults(
                healthHealed: 0,
                manaHealed: 0,
                isPotion: false,
                buffType: ModContent.BuffType<DummyBuff>(),
                buffTimeInSeconds: 0.5f
                );
        }

        public override bool CanUseItemEmotionBuffItem(Player player)
        {
            EmotionPlayer emotionPlayer = player.GetModPlayer<EmotionPlayer>();

            if (emotionPlayer.Emotion == EmotionType.ANGRY || emotionPlayer.Emotion == EmotionType.NONE) { return true; }
            return false;
        }

        public override bool? UseItemEmotionBuffItem(Player player)
        {

            EmotionHelper.ApplyOrPromoteBuff<AngryEmotionBase>(
                player: player,
                baseBuffType: ModContent.BuffType<Angry>(),
                duration: EmotionHelper.emotionTimeInSeconds * 60
                );

            return true;
        }
    }
}
