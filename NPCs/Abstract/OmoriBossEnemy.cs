using Terraria;
using Terraria.ID;

namespace OmoriMod.NPCs.Abstract
{
    public abstract class OmoriBossEnemy : OmoriModEnemy
    {

        /// <summary>
        /// Allows you to set all your NPC's properties, such as width, damage, aiStyle, lifeMax, etc. <br/>
        /// <see cref="NPC.boss"/> is already set to <c>true</c>
        /// </summary>
        public virtual void SetDefaultsBossEnemy() { }
        public override void SetDefaults()
        {
            NPC.boss = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            SetDefaultsBossEnemy();
        }
    }
}
