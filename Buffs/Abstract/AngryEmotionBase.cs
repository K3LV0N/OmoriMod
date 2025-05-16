using Terraria;
using Microsoft.Xna.Framework;
using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.Systems.EmotionSystem;
using System.Numerics;
using System;
using System.IO;
using OmoriMod.Players;

namespace OmoriMod.Buffs.Abstract
{
    public abstract class AngryEmotionBase : EmotionBuff
    {
        // damage increase
        public float Player_Damage_Increase_Percent => LogisticGrowthPerLevel(
            perLvl: Player_Damage_Increase_Per_Level,
            maxValue: Player_Max_Damage_Increase,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: Player_Damage_Increase_Starting_Value
            );
        public float NPC_Damage_Increase_Percent => LogisticGrowthPerLevel(
            perLvl: NPC_Damage_Increase_Per_Level,
            maxValue: NPC_Max_Damage_Increase,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: NPC_Damage_Increase_Starting_Value
            );

        // defense decrease
        public float Player_Defense_Decrease_Percent => LogisticGrowthPerLevel(
            perLvl: Player_Defense_Decrease_Per_Level,
            maxValue: Player_Max_Defense_Decrease,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: Player_Defense_Decrease_Starting_Value
            );
        public float NPC_Defense_Decrease_Percent => LogisticGrowthPerLevel(
            perLvl: NPC_Defense_Decrease_Per_Level,
            maxValue: NPC_Max_Defense_Decrease,
            emotionMidLevel: Mid_Emotion_Level,
            minValue: NPC_Defense_Decrease_Starting_Value
            );


        // Raw values

        // damage increase
        readonly private float Player_Damage_Increase_Per_Level;
        readonly private float Player_Damage_Increase_Starting_Value;
        readonly private float Player_Max_Damage_Increase;
        readonly private float NPC_Damage_Increase_Per_Level;
        readonly private float NPC_Damage_Increase_Starting_Value;
        readonly private float NPC_Max_Damage_Increase;

        // defense decrease
        readonly private float Player_Defense_Decrease_Per_Level;
        readonly private float Player_Defense_Decrease_Starting_Value;
        readonly private float Player_Max_Defense_Decrease;
        readonly private float NPC_Defense_Decrease_Per_Level;
        readonly private float NPC_Defense_Decrease_Starting_Value;
        readonly private float NPC_Max_Defense_Decrease;

        // other
        private int Mid_Emotion_Level;

        public AngryEmotionBase()
        {
            Emotion = EmotionType.ANGRY;
            dustColor = Color.Red;

            // Player Damage Increase
            Player_Damage_Increase_Per_Level        = 07.0f;
            Player_Damage_Increase_Starting_Value   = 05.0f;
            Player_Max_Damage_Increase              = 60.0f;

            // Player Defense Decrease
            Player_Defense_Decrease_Per_Level       = 12.5f;
            Player_Defense_Decrease_Starting_Value  = 12.5f;
            Player_Max_Defense_Decrease             = 99.9f;


            // NPC Damage Increase
            NPC_Damage_Increase_Per_Level           = 05.0f;
            NPC_Damage_Increase_Starting_Value      = 07.0f;
            NPC_Max_Damage_Increase                 = 50.0f;

            // NPC Defense Decrease
            NPC_Defense_Decrease_Per_Level          = 08.5f;
            NPC_Defense_Decrease_Starting_Value     = 03.5f;
            NPC_Max_Defense_Decrease                = 40.0f;
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {
            Mid_Emotion_Level = player.GetModPlayer<EmotionPlayer>().MidEmotionLevel;
            EmotionHelper.AngryBuffRemovals(player);
            EmotionHelper.AngryBuffModifiers(this, player);
        }

        public virtual void AngryModifyBuffText(ref string buffName, ref string tip, ref int rare) { }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            int damageUp = (int)MathF.Round(Player_Damage_Increase_Percent * 100);
            int defenseDown = (int)MathF.Round(Player_Defense_Decrease_Percent * 100);
            string buffTip = $"Attack up by {damageUp}%!" +
                $" Defense down by {defenseDown}%!";
            tip = buffTip;
            AngryModifyBuffText(ref buffName, ref tip, ref rare);
        }

        public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
        {
            EmotionHelper.AngryBuffRemovals(npc);
            EmotionHelper.AngryBuffModifiers(this,npc);  
        }
    }
}