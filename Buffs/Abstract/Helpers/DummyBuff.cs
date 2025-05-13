using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria;

namespace OmoriMod.Buffs.Abstract.Helpers
{
    /// <summary>
    /// A buff that does nothing. Used for custom buff logic (such as emotions).
    /// </summary>
    public class DummyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            return false;
        }
    }
}
