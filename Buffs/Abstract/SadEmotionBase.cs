using OmoriMod.Systems.EmotionSystem.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using System;
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
        public float playerPercentDefenseIncrease;
        public float NPCMaximumDefenseIncreaseThreshold;
        public float NPCPercentDefenseIncrease;

        // movement speed down
        public float playerPercentMovementSpeedDecrease;
        public float NPCPercentMovementSpeedDecrease;

        // damage to mana damage
        public float percentDamageToManaDamageConversion;

        public SadEmotionBase()
        {
            Emotion = EmotionType.SAD;
            dustColor = Color.Blue;
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
