using OmoriMod.Systems.EmotionSystem.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.NPCs.Global;

namespace OmoriMod.Buffs.Abstract
{
    /// <summary>
    /// Only Player speed changes here. Crits and Misses accounted for in <see cref="EmotionNPC"/>
    /// </summary>
    public abstract class HappyEmotionBase : EmotionBuff
    {
        // movement speed increase
        public float playerPercentMovementSpeedIncrease;
        public float NPCPercentMovementSpeedIncrease;

        // crit chance increase
        public float playerPercentExtraCritChance = 0.12f;
        public float NPCPercentExtraCritChance = 0.07f;

        // miss chance
        public float playerPercentMissChance;
        public float NPCPercentMissChance;

        public HappyEmotionBase() 
        {
            Emotion = EmotionType.HAPPY;
            dustColor = Color.Yellow;
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
