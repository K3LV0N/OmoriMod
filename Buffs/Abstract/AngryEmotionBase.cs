using Terraria;
using Microsoft.Xna.Framework;
using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.Systems.EmotionSystem;
using System;

namespace OmoriMod.Buffs.Abstract
{
    public abstract class AngryEmotionBase : EmotionBuff
    {

        // Player Configuration

        // ===== Damage Increase =====
        private const float PLAYER_DAMAGE_INCREASE_MAX = 60.0f;
        private const float PLAYER_DAMAGE_INCREASE_RATE = 7.0f;
        private const float PLAYER_DAMAGE_INCREASE_STARTING_VALUE = 2.0f;

        // ===== Defense Decrease =====
        private const float PLAYER_DEFENSE_DECREASE_MAX = 99.9f;
        private const float PLAYER_DEFENSE_DECREASE_RATE = 12.5f;
        private const float PLAYER_DEFENSE_DECREASE_STARTING_VALUE = 12.5f;




        // NPC Configuration

        // ===== Damage Increase =====
        private const float NPC_DAMAGE_INCREASE_MAX = 50.0f;
        private const float NPC_DAMAGE_INCREASE_RATE = 5.0f;
        private const float NPC_DAMAGE_INCREASE_STARTING_VALUE = 7.0f;

        // ===== Defense Decrease =====
        private const float NPC_DEFENSE_DECREASE_MAX = 40.0f;
        private const float NPC_DEFENSE_DECREASE_RATE = 8.5f;
        private const float NPC_DEFENSE_DECREASE_STARTING_VALUE = 3.5f;
        



        public float PLAYER_DAMAGE_INCREASE_PERCENT => LinearPerLevel(
            max: PLAYER_DAMAGE_INCREASE_MAX,
            rate: PLAYER_DAMAGE_INCREASE_RATE,
            maxEmotionLevel: EmotionHelper.PLAYER_MAX_EMOTION_LEVEL,
            startingValue: PLAYER_DAMAGE_INCREASE_STARTING_VALUE
            );

        public float NPC_DAMAGE_INCREASE_PERCENT => LinearPerLevel(
            max: NPC_DAMAGE_INCREASE_MAX,
            rate: NPC_DAMAGE_INCREASE_RATE,
            maxEmotionLevel: EmotionHelper.NPC_MAX_EMOTION_LEVEL,
            startingValue: NPC_DAMAGE_INCREASE_STARTING_VALUE
        );

        public float PLAYER_DEFENSE_DECREASE_PERCENT => LinearPerLevel(
            max: PLAYER_DEFENSE_DECREASE_MAX,
            rate: PLAYER_DEFENSE_DECREASE_RATE,
            maxEmotionLevel: EmotionHelper.PLAYER_MAX_EMOTION_LEVEL,
            startingValue: PLAYER_DEFENSE_DECREASE_STARTING_VALUE
        );

        public float NPC_DEFENSE_DECREASE_PERCENT => LinearPerLevel(
            max: NPC_DEFENSE_DECREASE_MAX,
            rate: NPC_DEFENSE_DECREASE_RATE,
            maxEmotionLevel: EmotionHelper.NPC_MAX_EMOTION_LEVEL,
            startingValue: NPC_DEFENSE_DECREASE_STARTING_VALUE
        );


        public AngryEmotionBase()
        {
            Emotion = EmotionType.ANGRY;
            dustColor = Color.Red;
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {
            EmotionHelper.AngryBuffRemovals(player);
            EmotionHelper.AngryDefenseModifiers(this, player);
        }

        public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
        {
            EmotionHelper.AngryBuffRemovals(npc);
            EmotionHelper.AngryDefenseModifiers(this, npc);
        }

        public virtual void AngryModifyBuffText(ref string buffName, ref string tip, ref int rare) { }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            int damageUp = (int)MathF.Round(PLAYER_DAMAGE_INCREASE_PERCENT * 100);
            int defenseDown = (int)MathF.Round(PLAYER_DEFENSE_DECREASE_PERCENT * 100);
            string buffTip = $"Attack up by {damageUp}%!" +
                $" Defense down by {defenseDown}%!";
            tip = buffTip;
            AngryModifyBuffText(ref buffName, ref tip, ref rare);
        }
    }
}