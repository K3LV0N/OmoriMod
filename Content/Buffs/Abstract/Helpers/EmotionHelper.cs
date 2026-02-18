using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using OmoriMod.Systems.EmotionSystem;
using System.Collections.Generic;
using OmoriMod.Content.NPCs.Global;
using OmoriMod.Content.Buffs.Abstract;
using OmoriMod.Content.Buffs.AngryBuff;
using OmoriMod.Content.Buffs.HappyBuff;
using OmoriMod.Content.Buffs.SadBuff;

namespace OmoriMod.Content.Buffs.Abstract.Helpers
{
    /// <summary>
    /// Class that handles all emotion related stat changes
    /// </summary>
    public static class EmotionHelper
    {

        public const int EMOTION_TIME_IN_SECONDS = 60;
        public const float EMOTIONAL_ADVANTAGE_VALUE_PER_LEVEL = 0.07f;
        public const int PLAYER_MAX_EMOTION_LEVEL = 43;
        public const int NPC_MAX_EMOTION_LEVEL = 1;


        public static readonly HashSet<int> TIER3_EMOTION_TYPES = [
            ModContent.BuffType<Furious>(),
            ModContent.BuffType<Manic>(),
            ModContent.BuffType<Miserable>(),
        ];

        public static readonly HashSet<int> TIER4_EMOTION_TYPES = [
            ModContent.BuffType<Livid>(),
            ModContent.BuffType<Hysterical>(),
            ModContent.BuffType<Despondent>(),
        ];



        /// <summary>
        /// returns the type of the <see cref="EmotionBuff"/> currently on the <see cref="Entity"/>. If no buff exists, returns null.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int? GetEmotionType(Entity entity)
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
            return null;
        }

        private static void RemoveEmotion(Entity entity, int emotionType)
        {
            if (entity is NPC npc)
            {
                if (Main.dedServ || Main.netMode == NetmodeID.SinglePlayer)
                {
                    npc.DelBuff(npc.FindBuffIndex(emotionType));
                }
                else
                {
                    npc.RequestBuffRemoval(emotionType);
                }
            }
            if (entity is Player player)
            {
                player.ClearBuff(emotionType);
            }
        }
        private static void RemoveEmotions<T1, T2>(Entity entity) 
            where T1 : EmotionBuff
            where T2 : EmotionBuff
        {
            if (entity is NPC npc)
            {
                foreach (int buffID in npc.buffType)
                {
                    if(ModContent.GetModBuff(buffID) is T1 || ModContent.GetModBuff(buffID) is T2)
                    {
                        RemoveEmotion(entity, buffID);
                    }   
                }
            }
            if (entity is Player player)
            {
                foreach (int buffID in player.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is T1 || ModContent.GetModBuff(buffID) is T2)
                    {
                        RemoveEmotion(entity, buffID);
                    }
                }
            }
        }

        public static void ClearAllEmotions(Entity entity)
        {
            if (entity is NPC npc)
            {
                foreach (int buffID in npc.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is EmotionBuff currentBuff)
                    {
                        RemoveEmotion(entity, buffID);
                    }
                }
            }
            if (entity is Player player)
            {
                foreach (int buffID in player.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is EmotionBuff currentBuff)
                    {
                        RemoveEmotion(entity, buffID);
                    }
                }
            }
        }


        public static void AngryBuffRemovals(Player player)
        {
            RemoveEmotions<SadEmotionBase, HappyEmotionBase>(player);
        }

        public static void AngryBuffRemovals(NPC npc)
        {
            RemoveEmotions<SadEmotionBase, HappyEmotionBase>(npc);
        }
        public static void HappyBuffRemovals(Player player)
        {
            RemoveEmotions<AngryEmotionBase, SadEmotionBase>(player);
        }
        public static void HappyBuffRemovals(NPC npc)
        {
            RemoveEmotions<AngryEmotionBase, SadEmotionBase>(npc);
        }
        public static void SadBuffRemovals(Player player)
        {
            RemoveEmotions<AngryEmotionBase, HappyEmotionBase>(player);
        }
        public static void SadBuffRemovals(NPC npc)
        {
            RemoveEmotions<AngryEmotionBase, HappyEmotionBase>(npc);
        }
        
        private static bool HasOtherEmotions<T1, T2>(Entity entity)
        {
            if (entity is NPC npc)
            {
                foreach (int buffID in npc.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is T1 || ModContent.GetModBuff(buffID) is T2)
                    {
                        return true;
                    }
                }
            }
            if (entity is Player player)
            {
                foreach (int buffID in player.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is T1 || ModContent.GetModBuff(buffID) is T2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the provided emmotion can be applied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="player"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static bool CanApplyEmotion<T>(Player player, int duration) where T : EmotionBuff
        {
            // check for other emotion types
            bool otherEmotions = false;
            if (typeof(AngryEmotionBase).IsAssignableFrom(typeof(T))) 
            {
                otherEmotions = HasOtherEmotions<HappyEmotionBase, SadEmotionBase>(player);
            }
            if (typeof(HappyEmotionBase).IsAssignableFrom(typeof(T)))
            {
                otherEmotions = HasOtherEmotions<AngryEmotionBase, SadEmotionBase>(player);
            }
            if (typeof(SadEmotionBase).IsAssignableFrom(typeof(T)))
            {
                otherEmotions = HasOtherEmotions<AngryEmotionBase, HappyEmotionBase>(player);
            }

            if (otherEmotions) { return false; }
            return true;
        }

        /// <summary>
        /// Applies the buff provided or promotes a pre-existing emotion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="player"></param>
        /// <param name="baseBuffType"></param>
        /// <param name="duration"></param>
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

        /// <summary>
        /// Special method to apply the tier 4 version of emotion buffs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="player"></param>
        /// <param name="baseBuffType"></param>
        /// <param name="duration"></param>
        public static void ApplyTier4Emotion<T>(Player player, int baseBuffType, int duration) where T : EmotionBuff
        {
            if (typeof(AngryEmotionBase).IsAssignableFrom(typeof(T))) AngryBuffRemovals(player);
            if (typeof(HappyEmotionBase).IsAssignableFrom(typeof(T))) HappyBuffRemovals(player);
            if (typeof(SadEmotionBase).IsAssignableFrom(typeof(T))) SadBuffRemovals(player);

            // Find any and tier 3 or lower buffs and remove them
            foreach (int buffID in player.buffType)
            {
                if (ModContent.GetModBuff(buffID) is T currentBuff)
                {
                    if (currentBuff.emotionLevel <= 3)
                    {
                        player.ClearBuff(buffID);
                    }
                }
            }

            player.AddBuff(baseBuffType, duration);
        }
    }
}