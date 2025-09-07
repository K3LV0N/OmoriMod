using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OmoriMod.Content.NPCs.Classes
{
    public static class ModNPCExtensions
    {

        /// <summary>
        /// Moves the <see cref="NPC"/> horizontally
        /// </summary>
        /// <param name="speed">The speed of the movement</param>
        /// <param name="inertia">How much inertia the <see cref="NPC"/> should have. This value should be greater than 1</param>
        /// <param name="xDirection">What direction the movement is going</param>
        public static void MoveHorizontal(this ModNPC npc, float speed, float inertia, int xDirection)
        {
            NPC n = npc.NPC;
            var direction = new Vector2(xDirection, 0);
            direction.Normalize();
            direction *= speed;
            n.velocity = (n.velocity * (inertia - 1) + direction * speed) / inertia;
        }

        /// <summary>
        /// Gets this <see cref="NPC"/> to send something into the chat
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="message"></param>
        public static void SpeakNPC(this ModNPC npc, string message)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(message);
                return;
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(message, []), Color.White, -1);
            }

        }

        /// <summary>
        /// Finds the distance between 2 sets of coordinates
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double FindDistance(this ModNPC npc, double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
