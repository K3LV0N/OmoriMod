using Microsoft.Xna.Framework;
using Terraria;
using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.Systems.EmotionSystem;

namespace OmoriMod.Buffs.Abstract
{
    public abstract class HappyEmotionBase : EmotionBuff
    {
        // movement speed increase
        public float Player_Movement_Speed_Increase_Percent => LinearPerLevel(Player_Movement_Speed_Increase_Per_Level, Player_Movement_Speed_Increase_Starting_Value);
        public float NPC_Movement_Speed_Increase_Percent => LinearPerLevel(NPC_Movement_Speed_Increase_Per_Level, NPC_Movement_Speed_Increase_Starting_Value);

        // crit chance increase
        public float Player_Extra_Crit_Chance_Percent => LogisticGrowthPerLevel(
            perLvl: Player_Extra_Crit_Chance_Per_Level,
            maxValue: Player_Max_Extra_Crit_Chance,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: Player_Extra_Crit_Chance_Starting_Value
            );
        public float NPC_Extra_Crit_Chance_Percent => LogisticGrowthPerLevel(
            perLvl: NPC_Extra_Crit_Chance_Per_Level,
            maxValue: NPC_Max_Extra_Crit_Chance,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: NPC_Extra_Crit_Chance_Starting_Value
            );

        // miss chance
        public float Player_Miss_Chance_Percent => LogisticGrowthPerLevel(
            perLvl: Player_Miss_Chance_Per_Level,
            maxValue: Player_Max_Miss_Chance,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: Player_Miss_Chance_Starting_Value
            );
        public float NPC_Miss_Chance_Percent => LogisticGrowthPerLevel(
            perLvl: NPC_Miss_Chance_Per_Level,
            maxValue: NPC_Max_Miss_Chance,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: NPC_Miss_Chance_Starting_Value
            );



        // movement speed increase
        readonly private float Player_Movement_Speed_Increase_Per_Level;
        readonly private float Player_Movement_Speed_Increase_Starting_Value;
        readonly private float NPC_Movement_Speed_Increase_Per_Level;
        readonly private float NPC_Movement_Speed_Increase_Starting_Value;

        // crit chance increase
        readonly private float Player_Extra_Crit_Chance_Per_Level;
        readonly private float Player_Extra_Crit_Chance_Starting_Value;
        readonly private float Player_Max_Extra_Crit_Chance;
        readonly private float NPC_Extra_Crit_Chance_Per_Level;
        readonly private float NPC_Extra_Crit_Chance_Starting_Value;
        readonly private float NPC_Max_Extra_Crit_Chance;


        // miss chance
        readonly private float Player_Miss_Chance_Per_Level;
        readonly private float Player_Miss_Chance_Starting_Value;
        readonly private float Player_Max_Miss_Chance;
        readonly private float NPC_Miss_Chance_Per_Level;
        readonly private float NPC_Miss_Chance_Starting_Value;
        readonly private float NPC_Max_Miss_Chance;

        // other
        readonly private int Mid_Emotion_Level;

        public HappyEmotionBase() 
        {
            Emotion = EmotionType.HAPPY;
            dustColor = Color.Yellow;

            // player movement speed increase
            Player_Movement_Speed_Increase_Per_Level        = 05.0f;
            Player_Movement_Speed_Increase_Starting_Value   = 05.0f;

            // player crit chance increase
            Player_Extra_Crit_Chance_Per_Level              = 06.5f;
            Player_Extra_Crit_Chance_Starting_Value         = 03.5f;
            Player_Max_Extra_Crit_Chance                    = 70.0f;

            // player miss chance
            Player_Miss_Chance_Per_Level                    = 08.5f;
            Player_Miss_Chance_Starting_Value               = 05.0f;
            Player_Max_Miss_Chance                          = 80.0f;


            // NPC movement speed increase
            NPC_Movement_Speed_Increase_Per_Level           = 03.5f;
            NPC_Movement_Speed_Increase_Starting_Value      = 04.5f;

            // NPC crit chance increase
            NPC_Extra_Crit_Chance_Per_Level                 = 05.0f;
            NPC_Extra_Crit_Chance_Starting_Value            = 03.0f;
            NPC_Max_Extra_Crit_Chance                       = 60.0f;

            // NPC miss chance
            NPC_Miss_Chance_Per_Level                       = 09.0f;
            NPC_Miss_Chance_Starting_Value                  = 04.0f;
            NPC_Max_Miss_Chance                             = 70.0f;

            // other
            Mid_Emotion_Level                               = 10;
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {
            EmotionHelper.HappyBuffRemovals(player);
            EmotionHelper.HappyBuffModifiers(this, player);
        }

        public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
        {
            EmotionHelper.HappyBuffRemovals(npc);
        }
    }
}
