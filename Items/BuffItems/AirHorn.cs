using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Players;
using OmoriMod.Systems.EmotionSystem.Interfaces;

namespace OmoriMod.Items.BuffItems
{
    public class AirHorn : AngryItem
    {
        public override void SetDefaults()
        {

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
                buffType: ModContent.BuffType<Angry>(),
                buffTimeInSeconds: 60
                );
        }

        public override bool CanUseItem(Player player)
        {
            EmotionPlayer emotionPlayer = player.GetModPlayer<EmotionPlayer>();

            if (emotionPlayer.Emotion == EmotionType.ANGRY || emotionPlayer.Emotion == EmotionType.NONE) { return true; }
            return false;
        }

        public override bool? UseItem(Player player)
        {
            if (player.HasBuff<Furious>())
            {
                player.AddBuff(ModContent.BuffType<Furious>(), Item.buffTime);
            }
            else if (player.HasBuff<Enraged>())
            {
                player.ClearBuff(ModContent.BuffType<Enraged>());
                player.AddBuff(ModContent.BuffType<Furious>(), Item.buffTime);
            }
            else if (player.HasBuff<Angry>())
            {
                player.ClearBuff(ModContent.BuffType<Angry>());
                player.AddBuff(ModContent.BuffType<Enraged>(), Item.buffTime);
            }
            else
            {
                player.AddBuff(ModContent.BuffType<Angry>(), Item.buffTime);
            }

            return true;
        }

    }
}
