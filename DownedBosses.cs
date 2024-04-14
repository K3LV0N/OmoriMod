using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;

namespace OmoriMod
{
    internal class DownedBosses : ModSystem
    {

        internal static bool downed_Sprout;


        public static bool downedSprout
        {
            get
            {
                return downed_Sprout;
            }
            set
            {
                if (!value)
                {
                    downed_Sprout = false;
                }
                else
                {
                    NPC.SetEventFlagCleared(ref downed_Sprout, -1);
                }
            }
        }
        internal static void Reset()
        {
            downed_Sprout = false;
        }

        public override void OnWorldLoad()
        {
            Reset();
        }

        public override void OnWorldUnload()
        {
            Reset();
        }

        public override void SaveWorldData(TagCompound tag)
        {
            List<string> downedBossList = new List<string>();
            
            if(downedSprout)
            {
                downedBossList.Add("YeOldSprout");
            }

            tag.Add("downedBossList", downedBossList);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            IList<string> downedlist = tag.GetList<string>("downedBossList");
            downedSprout = downedlist.Contains("YeOldSprout");
        }
    }
}