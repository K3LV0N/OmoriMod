using OmoriMod.Content.Items.BossRelated.BossSummons;
using OmoriMod.Content.NPCs.Enemies.Bosses.SweetHeart;
using OmoriMod.Content.NPCs.Enemies.Bosses.YeOldSprout;
using Terraria.ModLoader;

namespace OmoriMod
{
	
	public class OmoriMod : Mod
	{
        public static Mod modInstance;
        public static string MOD_NAME = "OmoriMod:";

        public OmoriMod()
		{
			modInstance = this;
		}

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("dementiaMod", out Mod dementiaMod))
			{
                dementiaMod.Call("AddBossSummon", ModContent.ItemType<MegaTofu>(), new int[] { ModContent.NPCType<YeOldSprout>() });
                dementiaMod.Call("AddBossSummon", ModContent.ItemType<SplinteredSweet>(), new int[] { ModContent.NPCType<SweetHeart>() });
            }
        }
    }
}