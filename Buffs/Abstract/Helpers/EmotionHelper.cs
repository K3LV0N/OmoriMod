using Terraria.ModLoader;
using Terraria;

namespace OmoriMod.Buffs.Abstract.Helpers
{
    public static class EmotionHelper
    {
        public static void ApplyOrPromoteBuff<T>(Player player, int baseBuffType, int duration) where T : EmotionBuff
        {
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
    }
}
