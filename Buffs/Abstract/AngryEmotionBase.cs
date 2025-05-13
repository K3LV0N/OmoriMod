using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Systems.EmotionSystem.Interfaces;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using OmoriMod.Systems.EmotionSystem;
using System;
using OmoriMod.Buffs.AngryBuff;

namespace OmoriMod.Buffs.Abstract
{
    /// <summary>
    /// Only defense alterations contained here. Damage increases are done in <see cref="EmotionNPC"/>
    /// </summary>
    public abstract class AngryEmotionBase : EmotionBuff
    {

        public float playerPercentDefenseDecrease;
        public float NPCMinimumDefenseIncreaseThreshold;
        public float NPCPercentDefenseDecrease;

        

        public AngryEmotionBase()
        {
            Emotion = EmotionType.ANGRY;
            dustColor = Color.Red;

            emotions =
            [
                ModContent.BuffType<Angry>(),
                ModContent.BuffType<Enraged>(),
                ModContent.BuffType<Furious>(),
            ];
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {
            // clear other emotions
            player.ClearBuff(ModContent.BuffType<Happy>());
            player.ClearBuff(ModContent.BuffType<Ecstatic>());
            player.ClearBuff(ModContent.BuffType<Manic>());

            player.ClearBuff(ModContent.BuffType<Sad>());
            player.ClearBuff(ModContent.BuffType<Depressed>());
            player.ClearBuff(ModContent.BuffType<Miserable>());

            player.statDefense -= (int)(player.statDefense * playerPercentDefenseDecrease);
        }

        public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
        {
            npc.RequestBuffRemoval(ModContent.BuffType<Sad>());
            npc.RequestBuffRemoval(ModContent.BuffType<Happy>());

            int decreasedDefense = npc.defense - (int)(npc.defDefense * (1 - NPCPercentDefenseDecrease));
            int defenseThreshold = (int)(npc.defDefense * NPCMinimumDefenseIncreaseThreshold);

            npc.defense = Math.Max(decreasedDefense, defenseThreshold);
        }
    }
}
