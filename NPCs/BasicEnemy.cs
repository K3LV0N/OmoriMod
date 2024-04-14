using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.NPCs
{
    public abstract class BasicEnemy : ModNPC
    {
        public virtual void moveHorizontalBlocky(float maxSpeed, float minSpeed, float accel, float fastAccel, int xDirection)
        {
            // If under max speed and above min speed, accel
            if (Math.Abs(NPC.velocity.X) < maxSpeed && Math.Abs(NPC.velocity.X) > minSpeed)
            {
                NPC.velocity.X += xDirection * accel;
            }
            else
            {
                // If going over max speed, go max speed
                if (Math.Abs(NPC.velocity.X) >= maxSpeed)
                {
                    NPC.velocity.X = xDirection * maxSpeed;
                }
                // If under min speed, accel faster
                else
                {
                    NPC.velocity.X += xDirection * fastAccel;
                }
            }
        }

        /// <summary>
        /// <c>moveHorizontal</c> is a movement function.
        /// <paramref name="inertia"/> needs to be greater than 1.
        /// <paramref name="speed"/> is the TOTAL speed.
        /// <paramref name="xDirection"/> is the direction.
        /// </summary>
        public virtual void moveHorizontal(float speed, float inertia, int xDirection)
        {
            Vector2 direction = new Vector2(xDirection, 0);
            direction.Normalize();
            direction *= speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + direction * speed) / inertia;
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
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(message, Array.Empty<object>()), Color.White, -1);
            }
            
        }

        public double FindDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
