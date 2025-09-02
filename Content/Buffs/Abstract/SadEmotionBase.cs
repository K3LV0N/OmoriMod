using Microsoft.Xna.Framework;
using Terraria;
using OmoriMod.Systems.EmotionSystem;
using System;
using OmoriMod.Content.NPCs.Global;
using OmoriMod.Content.Buffs.Abstract.Helpers;


namespace OmoriMod.Content.Buffs.Abstract
{
    /// <summary>
    /// Only Defense and Player speed changes here. Damage conversion and NPC speed changes accounted for in <see cref="EmotionNPC"/>
    /// </summary>
    public abstract class SadEmotionBase : EmotionBuff
    {

		// Player Configuration

		// ===== Movement Speed Decrease =====
		private const float PLAYER_MOVEMENT_SPEED_DECREASE_MAX = 80.0f;
		private const float PLAYER_MOVEMENT_SPEED_DECREASE_RATE = 5.0f;
		private const float PLAYER_MOVEMENT_SPEED_DECREASE_STARTING_VALUE = 6.0f;

		// ===== Defense Up =====
		private const float PLAYER_DEFENSE_INCREASE_MAX = 60.0f;
		private const float PLAYER_DEFENSE_INCREASE_RATE = 6.0f;
		private const float PLAYER_DEFENSE_INCREASE_STARTING_VALUE = 3.5f;

		// ===== Damage to Mana Damage =====
		private const float HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_MAX = 75.0f;
		private const float HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_RATE = 6.5f;
		private const float HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_STARTING_VALUE = 6.0f;




		// NPC Configuration

		// ===== Movement Speed Decrease =====
		private const float NPC_DEFENSE_INCREASE_MAX = 50.0f;
		private const float NPC_DEFENSE_INCREASE_RATE = 3.5f;
		private const float NPC_DEFENSE_INCREASE_STARTING_VALUE = 8.5f;

		// ===== Defense Up =====
		private const float NPC_MOVEMENT_SPEED_DECREASE_MAX = 60.0f;
		private const float NPC_MOVEMENT_SPEED_DECREASE_RATE = 4.0f;
		private const float NPC_MOVEMENT_SPEED_DECREASE_STARTING_VALUE = 7.0f;





        // defense up
        public float PLAYER_DEFENSE_INCREASE_PERCENT => LinearPerLevel(
            max: PLAYER_DEFENSE_INCREASE_MAX,
            rate: PLAYER_DEFENSE_INCREASE_RATE,
            maxEmotionLevel: EmotionHelper.PLAYER_MAX_EMOTION_LEVEL,
            startingValue: PLAYER_DEFENSE_INCREASE_STARTING_VALUE
            );
        public float NPC_DEFENSE_INCREASE_PERCENT => LinearPerLevel(
			max: NPC_DEFENSE_INCREASE_MAX,
			rate: NPC_DEFENSE_INCREASE_RATE,
			maxEmotionLevel: EmotionHelper.NPC_MAX_EMOTION_LEVEL,
			startingValue: NPC_DEFENSE_INCREASE_STARTING_VALUE
			);

		// movement speed down
		public float PLAYER_MOVEMENT_SPEED_DECREASE_PERCENT => LinearPerLevel(
			max: PLAYER_MOVEMENT_SPEED_DECREASE_MAX,
			rate: PLAYER_MOVEMENT_SPEED_DECREASE_RATE,
			maxEmotionLevel: EmotionHelper.PLAYER_MAX_EMOTION_LEVEL,
			startingValue: PLAYER_MOVEMENT_SPEED_DECREASE_STARTING_VALUE
			);
		public float NPC_MOVEMENT_SPEED_DECREASE_PERCENT => LinearPerLevel(
			max: NPC_MOVEMENT_SPEED_DECREASE_MAX,
			rate: NPC_MOVEMENT_SPEED_DECREASE_RATE,
			maxEmotionLevel: EmotionHelper.NPC_MAX_EMOTION_LEVEL,
			startingValue: NPC_MOVEMENT_SPEED_DECREASE_STARTING_VALUE
			);

		// damage to mana damage
		public float HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_PERCENT => LinearPerLevel(
			max: HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_MAX,
			rate: HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_RATE,
			maxEmotionLevel: EmotionHelper.PLAYER_MAX_EMOTION_LEVEL,
			startingValue: HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_STARTING_VALUE
			);



        public SadEmotionBase()
        {
            Emotion = EmotionType.SAD;
            dustColor = Color.Blue;
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {
            EmotionHelper.SadBuffRemovals(player);
            EmotionHelper.SadDefenseModifiers(this, player);
        }
		public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
		{
			EmotionHelper.SadBuffRemovals(npc);
			EmotionHelper.SadDefenseModifiers(this, npc);
		}

		public virtual void SadModifyBuffText(ref string buffName, ref string tip, ref int rare) { }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            int defenseUp = (int)MathF.Round(PLAYER_DEFENSE_INCREASE_PERCENT * 100);
            int speedDown = (int)MathF.Round(PLAYER_MOVEMENT_SPEED_DECREASE_PERCENT * 100);
            int mana = (int)MathF.Round(HEALTH_DAMAGE_TO_MANA_DAMAGE_CONVERSION_PERCENT * 100);
            string buffTip = $"Defense up by {defenseUp}%!" +
                $" Speed down by {speedDown}%!" +
                $" {mana}% of damage convertd to mana damage!";
            tip = buffTip;
            SadModifyBuffText(ref buffName, ref tip, ref rare);
        } 
    }
}
