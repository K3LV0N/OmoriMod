using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using OmoriMod.NPCs.Global;
using OmoriMod.Systems.EmotionSystem;
using System.Collections.Generic;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;

namespace OmoriMod.Buffs.Abstract.Helpers
{
    /// <summary>
    /// Class that handles all emotion related stat changes
    /// </summary>
    public static class EmotionHelper
    {
        public const int emotionTimeInSeconds = 60;

        public static readonly float EmotionalAdvantageValuePerLevel = 0.07f;

        public static readonly int MaxEmotionLevel = 43;

        public static readonly HashSet<int> Tier3EmotionTypes = [
            ModContent.BuffType<Furious>(),
            ModContent.BuffType<Manic>(),
            ModContent.BuffType<Miserable>(),
        ];

        public static readonly HashSet<int> Tier4EmotionTypes = [
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
                HappyEmotionBase happyEmotionBase = (HappyEmotionBase)ModContent.GetModBuff(GetEmotionType(npc).Value);
                if (happyEmotionBase != null) HappyBuffModifiers(happyEmotionBase, npc);
            }
            if (emotionNPC.Emotion == EmotionType.SAD)
            {
                SadEmotionBase sadEmotionBase = (SadEmotionBase)ModContent.GetModBuff(GetEmotionType(npc).Value);
                if (sadEmotionBase != null) SadBuffSpeedModifiers(sadEmotionBase, npc);
            }
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



        public static void AngryBuffModifiers(AngryEmotionBase angryEmotion, Player player)
        {
            player.statDefense -= (int)(player.statDefense * angryEmotion.Player_Defense_Decrease_Percent);
        }
        public static void AngryBuffModifiers(AngryEmotionBase angryEmotion, NPC npc)
        {
            int decreasedDefense = npc.defense - (int)(npc.defDefense * (1 - angryEmotion.NPC_Defense_Decrease_Percent));
            npc.defense = decreasedDefense;
        }
        public static void HappyBuffModifiers(HappyEmotionBase happyEmotion, Player player)
        {
            player.moveSpeed *= (1 + happyEmotion.Player_Movement_Speed_Increase_Percent);
        }
        public static void HappyBuffModifiers(HappyEmotionBase happyEmotion, NPC npc)
        {
            Vector2 newPos = CalculateNewPosition(npc, happyEmotion.NPC_Movement_Speed_Increase_Percent);

            // If the new speed collides with something, don't add it
            if (!Collision.SolidCollision(newPos, npc.width, npc.height))
            {
                npc.position = newPos;
            }
        }
        public static void SadBuffModifiers(SadEmotionBase sadEmotion, Player player)
        {
            player.statDefense += (int)(player.statDefense * sadEmotion.Player_Defense_Increase_Percent);
            player.moveSpeed *= (1 - sadEmotion.Player_Movement_Speed_Decrease_Percent);
        }
        public static void SadBuffModifiers(SadEmotionBase sadEmotion, NPC npc)
        {
            int increasedDefense = npc.defense * (int)(1 + sadEmotion.NPC_Defense_Increase_Percent);
            npc.defense = increasedDefense;
        }
        public static void SadBuffSpeedModifiers(SadEmotionBase sadEmotion, NPC npc)
        {
            npc.position = CalculateNewPosition(npc, -sadEmotion.NPC_Movement_Speed_Decrease_Percent);
        }



        public static void AngryHitModifiers(AngryEmotionBase angryEmotion, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= (1 + angryEmotion.Player_Damage_Increase_Percent);
        }
        public static void AngryHitModifiers(AngryEmotionBase angryEmotion, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= (1 + angryEmotion.NPC_Damage_Increase_Percent);
        }
        public static void HappyHitModifiers(HappyEmotionBase happyEmotion, ref NPC.HitModifiers modifiers)
        {
            // miss chance
            var noDMG = new StatModifier();
            noDMG *= 0;
            noDMG -= 1;
            modifiers.SourceDamage = Main.rand.NextFloat() < happyEmotion.Player_Miss_Chance_Percent ? noDMG : modifiers.SourceDamage;

            // extra crit chance
            if (Main.rand.NextFloat() < happyEmotion.Player_Extra_Crit_Chance_Percent)
            {
                modifiers.SetCrit();
            }
        }
        public static void HappyHitModifiers(HappyEmotionBase happyEmotion, ref Player.HurtModifiers modifiers)
        {
            // miss chance
            var noDMG = new StatModifier();
            noDMG *= 0;
            noDMG -= 1;
            modifiers.SourceDamage = Main.rand.NextFloat() < happyEmotion.Player_Miss_Chance_Percent ? noDMG : modifiers.SourceDamage;

            // extra crit chance
            if (Main.rand.NextFloat() < happyEmotion.Player_Extra_Crit_Chance_Percent && modifiers.SourceDamage != noDMG)
            {
                modifiers.SourceDamage *= 1.5f;
            }
        }
        public static void SadHitManaModifiers(Player player, SadEmotionBase sadEmotion, Player.HurtInfo hurtInfo)
        {
            float manaChange = hurtInfo.SourceDamage * sadEmotion.Damage_To_Mana_Damage_Conversion_Percent;
            if ((int)(player.statMana - manaChange) > 0)
            {
                player.statMana = (int)(player.statMana - manaChange);
            }
            else
            {
                player.statMana = 0;
            }
        }
        public static void SadHitDamageReductionModifiers(SadEmotionBase sadEmotion, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= (1 - sadEmotion.Damage_To_Mana_Damage_Conversion_Percent);
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