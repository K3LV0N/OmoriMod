using OmoriMod.Systems.EmotionSystem.Interfaces;
using Terraria;
using Microsoft.Xna.Framework;
using OmoriMod.Buffs.Abstract.Helpers;
using OmoriMod.NPCs.Global;

namespace OmoriMod.Buffs.Abstract
{
    /// <summary>
    /// Only defense alterations contained here. Damage increases are done in <see cref="EmotionNPC"/>
    /// </summary>
    public abstract class AngryEmotionBase : EmotionBuff
    {
        // damage increase
        public float playerPercentDamageIncrease;
        public float NPCPercentDamageIncrease;

        // defense decrease
        public float playerPercentDefenseDecrease;
        public float NPCMinimumDefenseIncreaseThreshold;
        public float NPCPercentDefenseDecrease;

        public AngryEmotionBase()
        {
            Emotion = EmotionType.ANGRY;
            dustColor = Color.Red;
        }

        public override void UpdateEmotionBuff(Player player, ref int buffIndex)
        {
            EmotionHelper.AngryBuffRemovals(player);
            EmotionHelper.AngryBuffModifiers(this, player);
        }

        public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
        {
            EmotionHelper.AngryBuffRemovals(npc);
            EmotionHelper.AngryBuffModifiers(this,npc);  
        }
    }
}
