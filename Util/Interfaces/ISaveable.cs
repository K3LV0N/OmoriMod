﻿using Terraria.ModLoader.IO;

namespace OmoriMod.Util.Interfaces
{
    /// <summary>
    /// A contract in for objects that need to be saved. 
    /// Promises that they are <see cref="TagCompound"/> serializable
    /// </summary>
    internal interface ISaveable
    {
        public void SaveData(TagCompound tag, string identifier);

        public void LoadData(TagCompound tag, string identifier);
    }
}
