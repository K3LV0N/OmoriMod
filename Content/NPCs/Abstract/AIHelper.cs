using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;

namespace OmoriMod.Content.NPCs.Abstract
{
    public static class AIHelper
    {

        /// <summary>
        /// Moves the <paramref name="npc"/> horizontally.
        /// </summary>
        /// <param name="npc">The <see cref="NPC"/> this method is being used on.</param>
        /// <param name="speed">The max speed of of this movment.</param>
        /// <param name="inertia">How much the movement should be resisted.</param>
        /// <param name="xDirection">The direction of the movement.</param>
        public static void MoveHorizontal(NPC npc, float speed, float inertia, int xDirection)
        {
            var direction = new Vector2(xDirection, 0);
            direction.Normalize();
            direction *= speed;
            npc.velocity = (npc.velocity * (inertia - 1) + direction * speed) / inertia;
        }

        public static void SpeakNPC(string message)
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

        public static double FindDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
