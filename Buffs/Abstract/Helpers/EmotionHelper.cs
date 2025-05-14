using Terraria.ModLoader;
using Terraria;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;
using System.Collections.Generic;
using OmoriMod.Buffs.AngryBuff;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
using OmoriMod.Systems.EmotionSystem.Interfaces;
using OmoriMod.NPCs.Global;

namespace OmoriMod.Buffs.Abstract.Helpers
{
    /// <summary>
    /// Class that handles all emotion related stat changes
    /// </summary>
    public static class EmotionHelper
    {
        public const int emotionTimeInSeconds = 60;

        static readonly List<int> angryBuffsToRemove = [
                ModContent.BuffType<Happy>(),
                ModContent.BuffType<Ecstatic>(),
                ModContent.BuffType<Manic>(),

                ModContent.BuffType<Sad>(),
                ModContent.BuffType<Depressed>(),
                ModContent.BuffType<Miserable>(),
                ];

        static readonly List<int> happyBuffsToRemove = [
                ModContent.BuffType<Angry>(),
                ModContent.BuffType<Enraged>(),
                ModContent.BuffType<Furious>(),

                ModContent.BuffType<Sad>(),
                ModContent.BuffType<Depressed>(),
                ModContent.BuffType<Miserable>(),
                ];

        static readonly List<int> sadBuffsToRemove   = [
                ModContent.BuffType<Angry>(),
                ModContent.BuffType<Enraged>(),
                ModContent.BuffType<Furious>(),

                ModContent.BuffType<Happy>(),
                ModContent.BuffType<Ecstatic>(),
                ModContent.BuffType<Manic>(),
                ];

        public static readonly List<float> EmotionalAdvantageValues = [
            0.05f,
            0.10f,
            0.15f,
            ];


        public static int GetEmotionType(Entity entity)
        {
            if (entity is NPC npc)
            {
                foreach (int buffID in npc.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is EmotionBuff currentBuff)
                    {
                        return currentBuff.Type;
                    }
                }
            }
            if (entity is Player player)
            {
                foreach (int buffID in player.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is EmotionBuff currentBuff)
                    {
                        return currentBuff.Type;
                    }
                }
            }
            return 0;
        }
        private static Vector2 CalculateNewPosition(NPC npc, float modifier)
        {
            Vector2 change;
            if (npc.noGravity) { change = npc.velocity * modifier; }
            else { change = new Vector2(npc.velocity.X * modifier, 0); }
            return npc.position + change;
        }
        public static void NPCMovementFromEmotion(NPC npc)
        {
            EmotionNPC emotionNPC = npc.GetGlobalNPC<EmotionNPC>();
            if (emotionNPC.Emotion == EmotionType.HAPPY)
            {
                HappyEmotionBase happyEmotionBase = (HappyEmotionBase)ModContent.GetModBuff(GetEmotionType(npc));
                if (happyEmotionBase != null) HappyBuffModifiers(happyEmotionBase, npc);
            }
            if (emotionNPC.Emotion == EmotionType.SAD)
            {
                SadEmotionBase sadEmotionBase = (SadEmotionBase)ModContent.GetModBuff(GetEmotionType(npc));
                if (sadEmotionBase != null) SadBuffSpeedModifiers(sadEmotionBase, npc);
            }
        }



        private static void RemovePlayerBuffs(Player player, List<int> buffsTypesToRemove)
        {
            foreach (int buffType in buffsTypesToRemove)
            {
                player.ClearBuff(buffType);
            }
        }
        private static void RemoveNPCBuffs(NPC npc, List<int> buffsTypesToRemove)
        {
            // DelBuff only for server or singleplayer client
            if (Main.dedServ || Main.netMode == NetmodeID.SinglePlayer)
            {
                foreach (int buffType in buffsTypesToRemove)
                {
                    if (npc.HasBuff(buffType)) npc.DelBuff(buffType);
                }
            }
            else
            {
                // RequestBuffRemoval for multiplayer client
                foreach (int buffType in buffsTypesToRemove)
                {
                    if (npc.HasBuff(buffType)) npc.RequestBuffRemoval(buffType);
                }
            }
        }



        public static void AngryBuffRemovals(Player player)
        {
            RemovePlayerBuffs(player, angryBuffsToRemove);
        }
        public static void AngryBuffRemovals(NPC npc)
        {
            RemoveNPCBuffs(npc, angryBuffsToRemove);
        }
        public static void HappyBuffRemovals(Player player)
        {   
            RemovePlayerBuffs(player, happyBuffsToRemove);
        }
        public static void HappyBuffRemovals(NPC npc)
        {
            RemoveNPCBuffs(npc, happyBuffsToRemove);
        }
        public static void SadBuffRemovals(Player player)
        {
            RemovePlayerBuffs(player, sadBuffsToRemove);
        }
        public static void SadBuffRemovals(NPC npc)
        {
            RemoveNPCBuffs(npc, sadBuffsToRemove);
        }



        public static void AngryBuffModifiers(AngryEmotionBase angryEmotion, Player player)
        {
            player.statDefense -= (int)(player.statDefense * angryEmotion.playerPercentDefenseDecrease);
        }
        public static void AngryBuffModifiers(AngryEmotionBase angryEmotion, NPC npc)
        {
            int decreasedDefense = npc.defense - (int)(npc.defDefense * (1 - angryEmotion.NPCPercentDefenseDecrease));
            int defenseThreshold = (int)(npc.defDefense * angryEmotion.NPCMinimumDefenseIncreaseThreshold);

            npc.defense = Math.Max(decreasedDefense, defenseThreshold);
        }
        public static void HappyBuffModifiers(HappyEmotionBase happyEmotion, Player player)
        {
            player.moveSpeed *= (1 + happyEmotion.playerPercentMovementSpeedIncrease);
        }
        public static void HappyBuffModifiers(HappyEmotionBase happyEmotion, NPC npc)
        {
            Vector2 newPos = CalculateNewPosition(npc, happyEmotion.NPCPercentMovementSpeedIncrease);

            // If the new speed collides with something, don't add it
            if (!Collision.SolidCollision(newPos, npc.width, npc.height))
            {
                npc.position = newPos;
            }
        }
        public static void SadBuffModifiers(SadEmotionBase sadEmotion, Player player)
        {
            player.statDefense += (int)(player.statDefense * sadEmotion.playerPercentDefenseIncrease);
            player.moveSpeed *= (1 - sadEmotion.playerPercentMovementSpeedDecrease);
        }
        public static void SadBuffModifiers(SadEmotionBase sadEmotion, NPC npc)
        {
            int increasedDefense = npc.defense * (int)(1 + sadEmotion.NPCPercentDefenseIncrease);
            int defenseThreshold = (int)(npc.defDefense * sadEmotion.NPCMaximumDefenseIncreaseThreshold);

            npc.defense = Math.Min(increasedDefense, defenseThreshold);
        }
        public static void SadBuffSpeedModifiers(SadEmotionBase sadEmotion, NPC npc)
        {
            npc.position = CalculateNewPosition(npc, -sadEmotion.NPCPercentMovementSpeedDecrease);
        }
        public static void AngryHitModifiers(AngryEmotionBase angryEmotion, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= (1 + angryEmotion.playerPercentDamageIncrease);
        }
        public static void AngryHitModifiers(AngryEmotionBase angryEmotion, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= (1 + angryEmotion.NPCPercentDamageIncrease);
        }

        

        public static void HappyHitModifiers(HappyEmotionBase happyEmotion, ref NPC.HitModifiers modifiers)
        {
            // miss chance
            StatModifier noDMG = modifiers.SourceDamage - .99f;
            modifiers.SourceDamage = Main.rand.NextFloat() < happyEmotion.playerPercentMissChance ? noDMG : modifiers.SourceDamage;

            // extra crit chance
            if (Main.rand.NextFloat() < happyEmotion.playerPercentExtraCritChance)
            {
                modifiers.SetCrit();
            }
        }
        public static void HappyHitModifiers(HappyEmotionBase happyEmotion, ref Player.HurtModifiers modifiers)
        {
            // miss chance
            StatModifier noDMG = modifiers.SourceDamage - .99f;
            modifiers.SourceDamage = Main.rand.NextFloat() < happyEmotion.playerPercentMissChance ? noDMG : modifiers.SourceDamage;

            // extra crit chance
            if (Main.rand.NextFloat() < happyEmotion.playerPercentExtraCritChance && modifiers.SourceDamage != noDMG)
            {
                modifiers.SourceDamage *= 1.5f;
            }
        }
        public static void SadHitModifiers(Player player, SadEmotionBase sadEmotion, Player.HurtInfo hurtInfo)
        {
            float manaChange = hurtInfo.SourceDamage * sadEmotion.percentDamageToManaDamageConversion;
            if ((int)(player.statMana - manaChange) > 0)
            {
                player.statMana = (int)(player.statMana - manaChange);
            }
            else
            {
                player.statMana = 0;
            }
        }
        


        public static void ApplyOrPromoteBuff<T>(Player player, int baseBuffType, int duration) where T : EmotionBuff
        {

            if (typeof(AngryEmotionBase).IsAssignableFrom(typeof(T))) AngryBuffRemovals(player);
            if (typeof(HappyEmotionBase).IsAssignableFrom(typeof(T))) HappyBuffRemovals(player);
            if (typeof(SadEmotionBase).IsAssignableFrom(typeof(T))) SadBuffRemovals(player);

            // Check which emotion buff the player currently has
            foreach (int buffID in player.buffType)
            {
                if (ModContent.GetModBuff(buffID) is T currentBuff)
                {
                    // Try to promote emotion
                    int? nextStage = currentBuff.nextStageEmotionType;
                    if (nextStage.HasValue)
                    {
                        player.ClearBuff(buffID);
                        player.AddBuff(nextStage.Value, duration);
                    } else
                    {
                        // reapply max lvl emotion
                        player.AddBuff(currentBuff.Type, duration);
                    }
                    return; // Handled, don't apply base buff
                }
            }

            // If no angry-type buff was found, apply base
            player.AddBuff(baseBuffType, duration);
        }
    }
}