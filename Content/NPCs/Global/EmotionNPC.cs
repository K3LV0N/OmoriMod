using Microsoft.Xna.Framework;
using OmoriMod.Content.Buffs.Abstract;
using OmoriMod.Content.Buffs.Abstract.Helpers;
using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.Players;
using OmoriMod.Systems.EmotionSystem;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Content.NPCs.Global
{
    public class EmotionNPC : GlobalNPC, IEmotionEntity
    {
        public override bool InstancePerEntity => true;
        public EmotionType Emotion { get; set; }
        public bool ImmuneToEmotionChange { get; set; }

        public int colorTimer;

        public Color? original_color;

        public float happyBuffNPCSpeedUp = 0.3f;
        public float sadBuffNPCSlowDown = 0.3f;

        public override void SetDefaults(NPC entity)
        {
            // by default, all enemies are able to be effected by emotions
            ImmuneToEmotionChange = false;

            ModNPC modNPC = NPCLoader.GetNPC(entity.type);
            if (modNPC == null || modNPC is not OmoriNPC)
            {
                // bosses are immune to emotions
                ImmuneToEmotionChange = entity.boss;
            }
        }

        public override void ResetEffects(NPC npc)
        {
            Emotion = EmotionType.NONE;
        }

        private static int GetEmotionLevel(Entity entity)
        {
            if (entity is NPC npc)
            {
                foreach (int buffID in npc.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is EmotionBuff currentBuff)
                    {
                        return currentBuff.emotionLevel;
                    }
                }
            }
            if (entity is Player player)
            {
                foreach (int buffID in player.buffType)
                {
                    if (ModContent.GetModBuff(buffID) is EmotionBuff currentBuff)
                    {
                        return currentBuff.emotionLevel;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Changes the color of the <paramref name="npc"/> depending on its <see cref="Emotion"/>
        /// </summary>
        /// <param name="npc">The <see cref="NPC"/> effected</param>
        private void NPCColorChangeFromEmotion(NPC npc)
        {
            if (original_color == null)
            {
                original_color = npc.color;
            }
            colorTimer++;
            Color colorNeeded;
            if (Emotion != EmotionType.NONE)
            {
                if (Emotion == EmotionType.ANGRY)
                {
                    colorNeeded = Color.Red;
                }
                else if (Emotion == EmotionType.SAD)
                {
                    colorNeeded = Color.Blue;
                }
                else
                {
                    colorNeeded = Color.Yellow;
                }
                // Flash emotion color and original color
                if (colorTimer > 60)
                {
                    npc.color = Color.Lerp(npc.color, (Color)original_color, 0.1f);

                    if (colorTimer > 90)
                    {
                        colorTimer = 0;
                    }
                }
                else
                {
                    npc.color = Color.Lerp(npc.color, colorNeeded, 0.1f);
                }
            }
            else
            {
                // if we need to fix the color then do it, otherwise don't mess with the color
                if (npc.color != (Color)original_color) { npc.color = Color.Lerp(npc.color, (Color)original_color, 0.1f); }
            }
        }


        public override void AI(NPC npc)
        {
            // Call movement here since it is an AI action (AKA if the npc is frozen or something, don't do this)
            EmotionHelper.NPCMovementFromEmotion(npc);
        }
        public override void PostAI(NPC npc)
        {
            // Color change happens regardless of what happens in PreAI or AI
            NPCColorChangeFromEmotion(npc);
        }


        /// <summary>
        /// Returns the emotional advantage level of the attacker and the target.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns><c>0</c> means no advantage. 
        /// Any <c>positive value</c> means the attacker has advantage. 
        /// Any <c>negative value</c> means the defender has advantage.</returns>
        private static int CalculateAdvantage(IEmotionEntity attacker, IEmotionEntity defender, Entity attackEntity, Entity defendEntity)
        {
            bool? attackerAdvantage = attacker.CheckForAdvantage(defender);
            if (attackerAdvantage == null) { return 0; }
            if (attackerAdvantage == true)
            {
                return GetEmotionLevel(attackEntity) - GetEmotionLevel(defendEntity) + 1;
            }
            else
            {
                return GetEmotionLevel(defendEntity) - GetEmotionLevel(attackEntity) + 1;
            }
        }

        private static void ApplyAdvantage(int advantage, ref NPC.HitModifiers modifiers)
        {
            if (advantage == 0) return;

            if (advantage > 0)
            {
                modifiers.SourceDamage += EmotionHelper.EMOTIONAL_ADVANTAGE_VALUE_PER_LEVEL * advantage;
                return;
            }

            if (advantage < 0)
            {
                modifiers.SourceDamage -= EmotionHelper.EMOTIONAL_ADVANTAGE_VALUE_PER_LEVEL * advantage;
                return;
            }
        }
        private static void ApplyAdvantage(int advantage, ref Player.HurtModifiers modifiers)
        {
            if (advantage == 0) return;

            if (advantage > 0)
            {
                int index = advantage - 1;
                modifiers.SourceDamage += EmotionHelper.EMOTIONAL_ADVANTAGE_VALUE_PER_LEVEL * advantage;
                return;
            }

            if (advantage < 0)
            {
                int index = -advantage - 1;
                modifiers.SourceDamage -= EmotionHelper.EMOTIONAL_ADVANTAGE_VALUE_PER_LEVEL * advantage;
                return;
            }
        }


        private static void ApplyAdditionalEmotionModifiers(IEmotionEntity attacker, Entity attackEntity, ref NPC.HitModifiers modifiers)
        {
            if (attacker.Emotion == EmotionType.ANGRY)
            {
                AngryEmotionBase angryEmotion = (AngryEmotionBase)ModContent.GetModBuff(EmotionHelper.GetEmotionType(attackEntity).Value);
                EmotionHelper.AngryDamageModifiers(angryEmotion, ref modifiers);
            }

            if (attacker.Emotion == EmotionType.HAPPY)
            {
                HappyEmotionBase happyEmotion = (HappyEmotionBase)ModContent.GetModBuff(EmotionHelper.GetEmotionType(attackEntity).Value);
                EmotionHelper.HappyHitModifiers(happyEmotion, ref modifiers);
            }
        }
        private static void ApplyAdditionalEmotionModifiers(IEmotionEntity attacker, IEmotionEntity defender, Entity attackEntity, Entity defendEntity, ref Player.HurtModifiers modifiers)
        {
            if (attacker.Emotion == EmotionType.ANGRY)
            {
                AngryEmotionBase angryEmotion = (AngryEmotionBase)ModContent.GetModBuff(EmotionHelper.GetEmotionType(attackEntity).Value);
                EmotionHelper.AngryDamageModifiers(angryEmotion, ref modifiers);
            }

            if (attacker.Emotion == EmotionType.HAPPY)
            {
                HappyEmotionBase happyEmotion = (HappyEmotionBase)ModContent.GetModBuff(EmotionHelper.GetEmotionType(attackEntity).Value);
                EmotionHelper.HappyHitModifiers(happyEmotion, ref modifiers);
            }

            if (defender.Emotion == EmotionType.SAD)
            {
                SadEmotionBase sadEmotion = (SadEmotionBase)ModContent.GetModBuff(EmotionHelper.GetEmotionType(defendEntity).Value);
                EmotionHelper.SadHitDamageReductionModifiers(sadEmotion, ref modifiers);
            }
        }

        private static void EmotionalAdvantage(IEmotionEntity attacker, IEmotionEntity defender, Entity attackEntity, Entity defendEntity, ref NPC.HitModifiers modifiers)
        {
            int advantage = CalculateAdvantage(
                attacker: attacker,
                defender: defender,
                attackEntity: attackEntity,
                defendEntity: defendEntity
                );
            ApplyAdvantage(advantage, ref modifiers);
            ApplyAdditionalEmotionModifiers(attacker, attackEntity, ref modifiers);
        }
        private static void EmotionalAdvantage(IEmotionEntity attacker, IEmotionEntity defender, Entity attackEntity, Entity defendEntity, ref Player.HurtModifiers modifiers)
        {
            int advantage = CalculateAdvantage(
                attacker: attacker,
                defender: defender,
                attackEntity: attackEntity,
                defendEntity: defendEntity
                );
            ApplyAdvantage(advantage, ref modifiers);
            ApplyAdditionalEmotionModifiers(attacker, defender, attackEntity, defendEntity, ref modifiers);
        }


        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            EmotionPlayer emotionPlayer = player.GetModPlayer<EmotionPlayer>();
            EmotionNPC emotionNPC = npc.GetGlobalNPC<EmotionNPC>();
            EmotionalAdvantage(
                attacker: emotionPlayer,
                defender: emotionNPC,
                attackEntity: player,
                defendEntity: npc,
                modifiers: ref modifiers
                );
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[projectile.owner];
            EmotionPlayer emotionPlayer = player.GetModPlayer<EmotionPlayer>();
            EmotionNPC emotionNPC = npc.GetGlobalNPC<EmotionNPC>();
            EmotionalAdvantage(
                attacker: emotionPlayer,
                defender: emotionNPC,
                attackEntity: player,
                defendEntity: npc,
                modifiers: ref modifiers
                );

        }
        public override void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
        {
            EmotionNPC emotionAttacker = npc.GetGlobalNPC<EmotionNPC>();
            EmotionNPC emotionDefender = target.GetGlobalNPC<EmotionNPC>();
            EmotionalAdvantage(
                attacker: emotionAttacker,
                defender: emotionDefender,
                attackEntity: npc,
                defendEntity: target,
                modifiers: ref modifiers
                );
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            EmotionPlayer emotionPlayer = target.GetModPlayer<EmotionPlayer>();
            EmotionNPC emotionNPC = npc.GetGlobalNPC<EmotionNPC>();
            base.ModifyHitPlayer(npc, target, ref modifiers);
            EmotionalAdvantage(
                attacker: emotionNPC,
                defender: emotionPlayer,
                attackEntity: npc,
                defendEntity: target,
                modifiers: ref modifiers
                );
        }


        // used for sad emotion mana conversion
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            EmotionPlayer emotionPlayer = target.GetModPlayer<EmotionPlayer>();
            if (emotionPlayer.Emotion == EmotionType.SAD)
            {
                SadEmotionBase sadEmotion = (SadEmotionBase)ModContent.GetModBuff(EmotionHelper.GetEmotionType(emotionPlayer.Player).Value);
                EmotionHelper.SadHitManaModifiers(emotionPlayer.Player, sadEmotion, hurtInfo);
            }
        }
    }
}
