using System.Collections.Generic;
using System.IO;
using System.Linq;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OmoriMod.Content.Systems;

public class DownedBossSystem : ModSystem
{
    // Keep a single set of IDs for all downed bosses
    private static readonly HashSet<string> Downed = new(System.StringComparer.OrdinalIgnoreCase);

    // --- Public API ---
    public static bool IsDowned(string id) => Downed.Contains(id);

    /// <summary>
    /// Marks a boss as downed. Returns true if this was the first time.
    /// Also triggers a world-data sync on server so clients update.
    /// </summary>
    public static bool MarkDowned(string id)
    {
        if (!Downed.Add(id))
            return false;

        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendData(MessageID.WorldData); // will invoke NetSend on server

        return true;
    }

    public static void ClearDowned(string id) => Downed.Remove(id);

    // --- Lifecycle ---
    public override void ClearWorld() => Downed.Clear(); // correct place to reset world state

    public override void SaveWorldData(TagCompound tag)
    {
        if (Downed.Count > 0)
            tag["downedBosses"] = Downed.ToList();
    }

    public override void LoadWorldData(TagCompound tag)
    {
        Downed.Clear();
        foreach (var s in tag.GetList<string>("downedBosses"))
            Downed.Add(s);
    }

    // --- Multiplayer sync ---
    public override void NetSend(BinaryWriter writer)
    {
        writer.Write(Downed.Count);
        foreach (var id in Downed)
            writer.Write(id);
    }

    public override void NetReceive(BinaryReader reader)
    {
        Downed.Clear();
        int n = reader.ReadInt32();
        for (int i = 0; i < n; i++)
            Downed.Add(reader.ReadString());
    }
}