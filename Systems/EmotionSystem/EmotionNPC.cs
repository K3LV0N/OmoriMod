using Microsoft.Xna.Framework;
using OmoriMod.Systems.EmotionSystem.Interfaces;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Systems.EmotionSystem
{
    public class EmotionNPC : GlobalNPC, IEmotionObject
    {
        public override bool InstancePerEntity => true;
        public EmotionType Emotion { get; set; }

        public int colorTimer;

        public Color? original_color;

        public float happyBuffNPCSpeedUp = 0.3f;
        public float sadBuffNPCSlowDown = 0.3f;

        public override void ResetEffects(NPC npc)
        {
            Emotion = EmotionType.NONE;
        }

        private Vector2 CalculateNewPosition(NPC npc) {

            float modifier = happyBuffNPCSpeedUp;
            if (Emotion == EmotionType.SAD) { modifier = -sadBuffNPCSlowDown; }

            Vector2 change;
            if (npc.noGravity) { change = npc.velocity * modifier; }
            else { change = new Vector2(npc.velocity.X * modifier, 0); }

            return npc.position + change;
        }

        /// <summary>
        /// Changes how 'fast' the <paramref name="npc"/> is depending on its <see cref="Emotion"/>
        /// </summary>
        /// <param name="npc">The <see cref="NPC"/> effected</param>
        private void NPCMovementFromEmotion(NPC npc)
        {
            if (Emotion == EmotionType.HAPPY)
            {
                Vector2 newPos = CalculateNewPosition(npc);

                // If the new speed collides with something, don't add it
                if (!Collision.SolidCollision(newPos, npc.width, npc.height))
                {
                    npc.position = newPos;
                }
            }
            else if (Emotion == EmotionType.SAD)
            {
                npc.position = CalculateNewPosition(npc);
            }
        }

        /// <summary>
        /// Changes the color of the <paramref name="npc"/> depending on its <see cref="Emotion"/>
        /// </summary>
        /// <param name="npc">The <see cref="NPC"/> effected</param>
        private void NPCColorChangeFromEmotion(NPC npc)
        {
            if (original_color == null)
            {
                original_color = npc.color;
            }
            colorTimer++;
            Color colorNeeded;
            if (Emotion != EmotionType.NONE)
            {
                if (Emotion == EmotionType.ANGRY)
                {
                    colorNeeded = Color.Red;
                }
                else if (Emotion == EmotionType.SAD)
                {
                    colorNeeded = Color.Blue;
                }
                else
                {
                    colorNeeded = Color.Yellow;
                }
                // Flash emotion color and original color
                if (colorTimer > 60)
                {
                    npc.color = Color.Lerp(npc.color, (Color)original_color, 0.1f);

                    if (colorTimer > 90)
                    {
                        colorTimer = 0;
                    }
                }
                else
                {
                    npc.color = Color.Lerp(npc.color, colorNeeded, 0.1f);
                }
            }
            else
            {
                // if we need to fix the color then do it, otherwise don't mess with the color
                if (npc.color != (Color)original_color) { npc.color = Color.Lerp(npc.color, (Color)original_color, 0.1f); }
            }
        }


        public override void AI(NPC npc)
        {
            // Call movement here since it is an AI action (AKA if the npc is frozen or something, don't do this)
            NPCMovementFromEmotion(npc);
        }
        public override void PostAI(NPC npc)
        {
            // Color change happens regardless of what happens in PreAI or AI
            NPCColorChangeFromEmotion(npc);
        }
    }
}
