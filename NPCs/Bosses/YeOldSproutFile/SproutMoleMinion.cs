using System;
using Terraria;
using Terraria.ID;
using System.IO;

namespace OmoriMod.NPCs.Bosses.YeOldSproutFile
{
    internal class SproutMoleMinion : BasicEnemy
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 9;
        }

        public override void SetDefaults()
        {
            NPC.width = 17;
            NPC.height = 30;
            NPC.lifeMax = 30;

            NPC.damage = 6;
            NPC.defense = 2;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath9;

            NPC.value = 0f;
            NPC.knockBackResist = 1f;
            NPC.aiStyle = -1;
            NPC.netUpdate = true;
        }

        public float Stuck_Jump_Timer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public float AIPreviousXPosition;
        public bool jumping = false;

        public override void SendExtraAI(BinaryWriter writer)
        {            
            writer.Write(AIPreviousXPosition);
            writer.Write(jumping);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            AIPreviousXPosition = reader.ReadSingle();
            jumping = reader.ReadBoolean();
        }

        public override void AI()
        {
            NPC.netUpdate = true;
            NPC.TargetClosest(true);

            // Gravity
            float gravity = 0.2f;
            NPC.velocity.Y += gravity;

            // Variable for direction 
            int xDirection = 1;
            if (NPC.direction < 0)
            {
                xDirection = -1;
            }

            if (Stuck_Jump_Timer < 0)
            {
                NPC.velocity.X += 0.2f * xDirection;
            }
            else
            {
                // Variables for speed
                float initialJump = -16f;
                //float maxSpeed = 4f;
                float maxJumpSpeed = 6f;
                //float minSpeed = 1f;
                float accel = 0.05f;
                float fastAccel = accel * 2;
                // Variables for speed

                if (jumping)
                {

                    if (Math.Abs(NPC.velocity.X) < maxJumpSpeed)
                    {
                        NPC.velocity.X += xDirection * fastAccel;
                    }
                    else
                    {
                        NPC.velocity.X = xDirection * maxJumpSpeed;
                    }

                    if (Stuck_Jump_Timer % 20 == 0)
                    {
                        jumping = false;
                    }

                }
                else if (Stuck_Jump_Timer > 20 && Stuck_Jump_Timer % 20 == 0)
                {
                    if (NPC.position.X == AIPreviousXPosition)
                    {

                        NPC.velocity.Y += initialJump;
                        jumping = true;
                        Stuck_Jump_Timer = 0;
                    }
                    AIPreviousXPosition = NPC.position.X;

                }

                if (!jumping)
                {
                    float speed = 2f;
                    float inertia = 25f;

                    moveHorizontal(speed, inertia, xDirection);
                }
            }

            Stuck_Jump_Timer++;
        }

        private const int frame1 = 0;
        private const int frame2 = 1;
        private const int frame3 = 2;
        private const int frame4 = 3;
        private const int frame5 = 4;
        private const int frame6 = 5;
        private const int frame7 = 6;
        private const int frame8 = 7;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;

            if (NPC.frameCounter < 10)
            {
                NPC.frame.Y = frame1 * frameHeight;
            }
            else if (NPC.frameCounter < 20)
            {
                NPC.frame.Y = frame2 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = frame3 * frameHeight;
            }
            else if (NPC.frameCounter < 40)
            {
                NPC.frame.Y = frame4 * frameHeight;
            }
            else if (NPC.frameCounter < 50)
            {
                NPC.frame.Y = frame5 * frameHeight;
            }
            else if (NPC.frameCounter < 60)
            {
                NPC.frame.Y = frame6 * frameHeight;
            }
            else if (NPC.frameCounter < 70)
            {
                NPC.frame.Y = frame7 * frameHeight;
            }
            else if (NPC.frameCounter < 80)
            {
                NPC.frame.Y = frame8 * frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }

        }
    }
}
