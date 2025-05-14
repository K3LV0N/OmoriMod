using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using OmoriMod.Items.Health;
using System.IO;
using OmoriMod.Items.BuffItems;
using OmoriMod.NPCs.Abstract;

namespace OmoriMod.NPCs.enemies
{
    internal class SproutMole : OmoriModEnemy
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 9;
        }

        public override void SetDefaults()
        {
            NPC.width = 17;
            NPC.height = 30;
            NPC.lifeMax = 40;

            NPC.damage = 10;
            NPC.defense = 4;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath9;

            NPC.value = 10f;
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = -1;
            NPC.netUpdate = true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tofu>(), 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AirHorn>(), 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PartyPopper>(), 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainCloud>(), 5));
        }
        

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnModifier = .4f;
            // good chance of spawning if day on surface and underground / Caverns
            return SpawnCondition.OverworldDaySlime.Chance * spawnModifier + 
                SpawnCondition.Underground.Chance * spawnModifier + 
                SpawnCondition.Cavern.Chance * spawnModifier;
        }

        private const int State_Docile = 0;
        private const int State_Surprise = 1;
        private const int State_Attack = 2;

        public float AI_State
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public float AI_Timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
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
            if (AI_State == State_Docile && !NPC.justHit)
            {

                NPC.TargetClosest(false);
                if (AI_Timer == 0)
                {
                    bool leftOrRight = Main.rand.NextBool(2) ? true : false;

                    if (leftOrRight)
                    {
                        NPC.direction = 1;
                    }
                    else
                    {
                        NPC.direction = -1;
                    }
                }
                AI_Timer++;

                if (AI_Timer < 120)
                {
                    NPC.velocity.X = NPC.direction;
                    NPC.velocity.Y = 8f;

                    if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < 500f)
                    {
                        AI_State = State_Surprise;
                        AI_Timer = 0;
                    }

                }
                else
                {
                    AI_Timer = 0;
                }
            }
            else if (AI_State == State_Surprise || NPC.justHit)
            {
                NPC.TargetClosest(true);
                
                if(NPC.justHit)
                {
                    AI_State = State_Surprise;
                }

                if (AI_Timer == 0)
                {
                    NPC.velocity.Y = -3f;
                }

                if (NPC.velocity.Y <= 0)
                {
                    NPC.velocity.Y += 0.05f;
                }

                if (NPC.velocity.Y > 0)
                {
                    AIPreviousXPosition = NPC.position.X;
                    AI_State = State_Attack;
                    AI_Timer = 0;
                }

                AI_Timer++;
            }
            else if (AI_State == State_Attack)
            {
                AI_Timer++;
                NPC.TargetClosest(true);

                if (Main.player[NPC.target].Distance(NPC.Center) > 800f)
                {
                    AI_State = State_Docile;
                }
                else
                {

                    // gravity
                    if (NPC.velocity.Y >= 8)
                    {
                        NPC.velocity.Y = 8f;
                    }
                    else
                    {
                        NPC.velocity.Y += .25f;
                    }
                    

                    if (jumping)
                    {
                        int xDirection = 1;
                        if (NPC.direction < 0)
                        {
                            xDirection = -1;
                        }

                        NPC.velocity.X += xDirection * .25f;

                        if (AI_Timer % 15 == 0)
                        {
                            jumping = false;
                        }

                    }
                    else if (AI_Timer > 20 && AI_Timer % 20 == 0)
                    {
                        if (NPC.position.X == AIPreviousXPosition)
                        {

                            NPC.velocity.X += NPC.direction * 4f;
                            NPC.velocity.Y += -8f;
                            jumping = true;
                            AI_Timer = 0;
                        }
                        AIPreviousXPosition = NPC.position.X;

                    }
                    else
                    {

                        int xDirection = 1;

                        if (NPC.direction < 0)
                        {
                            xDirection = -1;
                        }
                        // Variables for speed
                        //float maxSpeed = 4f;
                        //float minSpeed = 1f;
                        //float accel = 0.05f;
                        //float fastAccel = 0.08f;
                        // Variables for speed

                        float speed = 2f;
                        float inertia = 25f;

                        MoveHorizontal(speed, inertia, xDirection);
                    }
                }
            }
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

            if (AI_State == State_Surprise)
            {
                NPC.frame.Y = frame3 * frameHeight;
                NPC.frameCounter = 30;
            }

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
