using OmoriMod.Systems.EmotionSystem.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.NPCs.Global;


namespace OmoriMod.Buffs.Abstract
{
    /// <summary>
    /// Only Defense and Player speed changes here. Damage conversion and NPC speed changes accounted for in <see cref="EmotionNPC"/>
    /// </summary>
    public abstract class SadEmotionBase : EmotionBuff
    {

        // defense up
        public float Player_Defense_Increase_Percent => LogisticGrowthPerLevel(
            perLvl: Player_Defense_Increase_Per_Level,
            maxValue: Player_Max_Defense_Increase,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: Player_Defense_Increase_Starting_Value
            );
        public float NPC_Defense_Increase_Percent => LogisticGrowthPerLevel(
            perLvl: NPC_Defense_Increase_Per_Level,
            maxValue: NPC_Max_Defense_Increase,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: NPC_Defense_Increase_Starting_Value
            );

        // movement speed down
        public float Player_Movement_Speed_Decrease_Percent => LogisticGrowthPerLevel(
            perLvl: Player_Movement_Speed_Decrease_Per_Level,
            maxValue: Player_Max_Movement_Speed_Decrease,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: Player_Movement_Speed_Decrease_Starting_Value
            );
        public float NPC_Movement_Speed_Decrease_Percent => LogisticGrowthPerLevel(
            perLvl: NPC_Movement_Speed_Decrease_Per_Level,
            maxValue: NPC_Max_Movement_Speed_Decrease,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: NPC_Movement_Speed_Decrease_Starting_Value
            );

        // damage to mana damage
        public float Damage_To_Mana_Damage_Conversion_Percent => LogisticGrowthPerLevel(
            perLvl: Damage_To_Mana_Damage_Conversion_Per_Level,
            maxValue: Max_Damage_To_Mana_Damage_Conversion,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: Damage_To_Mana_Damage_Conversion_Starting_Value
            );

        // defense up
        readonly private float Player_Defense_Increase_Per_Level;
        readonly private float Player_Defense_Increase_Starting_Value;
        readonly private float Player_Max_Defense_Increase;
        readonly private float NPC_Defense_Increase_Per_Level;
        readonly private float NPC_Defense_Increase_Starting_Value;
        readonly private float NPC_Max_Defense_Increase;

        // movement speed down
        readonly private float Player_Movement_Speed_Decrease_Per_Level;
        readonly private float Player_Movement_Speed_Decrease_Starting_Value;
        readonly private float Player_Max_Movement_Speed_Decrease;
        readonly private float NPC_Movement_Speed_Decrease_Per_Level;
        readonly private float NPC_Movement_Speed_Decrease_Starting_Value;
        readonly private float NPC_Max_Movement_Speed_Decrease;

        // damage to mana damage
        readonly private float Damage_To_Mana_Damage_Conversion_Per_Level;
        readonly private float Damage_To_Mana_Damage_Conversion_Starting_Value;
        readonly private float Max_Damage_To_Mana_Damage_Conversion;

        // other
        readonly private int Mid_Emotion_Level;

        public SadEmotionBase()
        {
            Emotion = EmotionType.SAD;
            dustColor = Color.Blue;

            // player defense increase
            Player_Defense_Increase_Per_Level               = 07.5f;
            Player_Defense_Increase_Starting_Value          = 05.5f;
            Player_Max_Defense_Increase                     = 70.0f;

            // player movement speed increase
            Player_Movement_Speed_Decrease_Per_Level        = 05.0f;
            Player_Movement_Speed_Decrease_Starting_Value   = 07.0f;
            Player_Max_Movement_Speed_Decrease              = 80.0f;

            // npc defense increase
            NPC_Defense_Increase_Per_Level                  = 03.5f;
            NPC_Defense_Increase_Starting_Value             = 08.5f;
            NPC_Max_Defense_Increase                        = 50.0f;

            // npc movement speed increase
            NPC_Movement_Speed_Decrease_Per_Level           = 04.0f;
            NPC_Movement_Speed_Decrease_Starting_Value      = 07.0f;
            NPC_Max_Movement_Speed_Decrease                 = 70.0f;

            // damage to mana damage
            Damage_To_Mana_Damage_Conversion_Per_Level      = 06.5f;
            Damage_To_Mana_Damage_Conversion_Starting_Value = 08.0f;
            Max_Damage_To_Mana_Damage_Conversion            = 75.0f;

            // Other
            Mid_Emotion_Level                               = 10;
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {
            EmotionHelper.SadBuffRemovals(player);
            EmotionHelper.SadBuffModifiers(this, player);
        }

        public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
        {
            EmotionHelper.SadBuffRemovals(npc);
            EmotionHelper.SadBuffModifiers(this, npc);
        }
    }
}
