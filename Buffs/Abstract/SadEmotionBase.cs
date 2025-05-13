using OmoriMod.Systems.EmotionSystem.Interfaces;
using Microsoft.Xna.Framework;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using Terraria.ModLoader;
using Terraria;
using System;
using OmoriMod.Systems.EmotionSystem;
using OmoriMod.Buffs.SadBuff;


namespace OmoriMod.Buffs.Abstract
{
    /// <summary>
    /// Only Defense and Player speed changes here. Damage conversion and NPC speed changes accounted for in <see cref="EmotionNPC"/>
    /// </summary>
    public abstract class SadEmotionBase : EmotionBuff
    {
        public float playerPercentMovementSpeedDecrease;
        public float playerPercentDefenseIncrease;
        public float NPCMaximumDefenseIncreaseThreshold;
        public float NPCPercentDefenseIncrease;
        public SadEmotionBase()
        {
            Emotion = EmotionType.SAD;
            dustColor = Color.Blue;
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {

            player.ClearBuff(ModContent.BuffType<Angry>());
            player.ClearBuff(ModContent.BuffType<Enraged>());
            player.ClearBuff(ModContent.BuffType<Furious>());

            player.ClearBuff(ModContent.BuffType<Happy>());
            player.ClearBuff(ModContent.BuffType<Ecstatic>());
            player.ClearBuff(ModContent.BuffType<Manic>());

            player.statDefense += (int)(player.statDefense * playerPercentDefenseIncrease);

            player.moveSpeed *= (1 - playerPercentMovementSpeedDecrease);
        }

        public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
        {
            npc.RequestBuffRemoval(ModContent.BuffType<Angry>());
            npc.RequestBuffRemoval(ModContent.BuffType<Happy>());

            int increasedDefense = npc.defense * (int)(1 + NPCPercentDefenseIncrease);
            int defenseThreshold = (int)(npc.defDefense * NPCMaximumDefenseIncreaseThreshold);

            npc.defense = Math.Min(increasedDefense, defenseThreshold);
        }
    }
}
