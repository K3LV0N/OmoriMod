using Microsoft.Xna.Framework;
using OmoriMod.Content.Items.BossRelated.BossBags;
using OmoriMod.Content.Items.BossRelated.YeOldSproutWeapons;
using OmoriMod.Content.Items.Health;
using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Systems;
using OmoriMod.Util;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Content.NPCs.Enemies.Bosses.YeOldSprout
{
    [AutoloadBossHead]

    public class YeOldSprout : OmoriBossEnemy
    {
        public YeOldSprout()
        {
            bossName = "YeOldSprout".OmoriModString();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 24;
        }
        public override void SetDefaultsBossEnemy()
        {
            NPC.width = 80;
            NPC.height = 80;
            NPC.lifeMax = 1000;

            NPC.damage = 15;
            NPC.defense = 10;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath9;

            NPC.value = 10f;
            NPC.knockBackResist = 0.25f;
            NPC.aiStyle = -1;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            // The order in which you add loot will appear as such in the Bestiary. To mirror vanilla boss order:
            // 1. Trophy
            // 2. Classic Mode ("not expert")
            // 3. Expert Mode (usually just the treasure bag)
            // 4. Master Mode (relic first, pet last, everything else in between)

            // How to drop things
            // npcLoot.Add(ItemDropRule.Common(itemId, chanceDenominator, minimumDropped, maximumDropped))

            // ItemDropRule.BossBag()
            // ItemDropRule.MasterModeCommonDrop()
            // ItemDropRule.MasterModeDropOnAllPlayers()


            // All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());



            // Add all drops except for the bag. These drops only get added if the difficulty is not expert

            int[] weaponOptions = [
                ModContent.ItemType<SproutShotgun>(), 
                ModContent.ItemType<SproutScythe>()
            ];
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, weaponOptions));


            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.LesserHealingPotion, 1, 5, 9));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Tofu>(), 1, 10, 25));
            notExpertRule.OnSuccess(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<YeOldSprout>()));

            // add non expert drops
            npcLoot.Add(notExpertRule);

            // add expert drops
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<YeOldBossBag>()));

        }
    

        public override void OnKill()
        {
            DownedBossSystem.MarkDowned(bossName);
        }
        public float Attack_Timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public float Jump_Timer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        public float Stuck_Jump_Timer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        public bool readyToFight = false;
        public bool jumping = false;
        public float AIPreviousXPosition;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(readyToFight);
            writer.Write(jumping);
            writer.Write(AIPreviousXPosition);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            readyToFight = reader.ReadBoolean();
            jumping = reader.ReadBoolean();
            AIPreviousXPosition = reader.ReadSingle();
        }

        // Runs at you for a bit, jumps afterwords. Occasionally stops and
        // Spawns sprout moles
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.netUpdate = true;
            choosePlayer();

            if (!readyToFight)
            {
                readyToFight = SpawnHelp(player);

                if (readyToFight)
                {
                    NPC.noGravity = false;
                    NPC.noTileCollide = false;
                    NPC.friendly = false;
                }
            }

            bool act = CheckForDespawn(player);

            if (readyToFight)
            {
                
                if (act)
                {
                    if (NPC.noTileCollide)
                    {
                        readyToFight = false;
                    }
                }

                if (act && readyToFight)
                {
                    // Gravity
                    float gravity = 0.4f;
                    NPC.velocity.Y += gravity;

                    // Variables for attack times
                    float runAttack = 360;
                    float jumpAttack = runAttack * 2;
                    // Variables for attack times

                    // Reset attack timer
                    if (Attack_Timer >= jumpAttack)
                    {
                        Attack_Timer = 0;
                    }

                    // Variables for speed
                    float initialJump = -16f;
                    float maxSpeed = 3f;
                    float maxJumpSpeed = 5f;
                    float minSpeed = 0.7f;
                    float accel = 0.03f;
                    float fastAccel = accel * 1.5f;
                    // Variables for speed

                    // Variable for direction 
                    int xDirection = 1;
                    if (NPC.direction < 0)
                    {
                        xDirection = -1;
                    }
                    // Variable for direction 

                    // If far enough away, move faster
                    float distance = Math.Abs(NPC.position.X - player.position.X);
                    float maxDistance = 600;
                    // A lower speedIncreaseRatio means a higher speed
                    float ratio = 5f;
                    float speedIncreaseRatio = (distance - maxDistance) / ratio;
                    if (distance > maxDistance)
                    {
                        maxSpeed *= speedIncreaseRatio;
                        maxJumpSpeed *= speedIncreaseRatio;
                        minSpeed *= speedIncreaseRatio;
                        accel *= speedIncreaseRatio;
                        fastAccel *= speedIncreaseRatio;
                    }

                    // Every 4 seconds
                    float minionSpawnTime = jumpAttack / 3f;

                    if (Attack_Timer % minionSpawnTime == 0)
                    {
                        bool spawnOrNot = Main.rand.NextBool(2);
                        if (spawnOrNot)
                        {
                            int xCenter = (int)(NPC.position.X + NPC.width / 2);
                            int yCenter = (int)(NPC.position.Y + NPC.height / 2);
                            int sprout1index = NPC.NewNPC(NPC.GetSource_FromAI(), xCenter, yCenter, ModContent.NPCType<SproutMoleMinion>(), 0, -30);
                            int sprout2index = NPC.NewNPC(NPC.GetSource_FromAI(), xCenter, yCenter, ModContent.NPCType<SproutMoleMinion>(), 0, -30);

                            NPC sprout1 = Main.npc[sprout1index];
                            NPC sprout2 = Main.npc[sprout2index];
                            sprout1.velocity = new Vector2(-2f, -6f);
                            sprout2.velocity = new Vector2(2f, -6f);
                        }
                    }

                    if (Attack_Timer < runAttack)
                    {
                        RunAtPlayer(player, initialJump, maxSpeed, maxJumpSpeed, minSpeed, fastAccel, accel, xDirection);
                        // Reset the Jump_Timer after JumpAtPlayer concludes
                        Jump_Timer = 0;
                    }
                    else if (Attack_Timer < jumpAttack)
                    {
                        JumpAtPlayer(player, initialJump, maxSpeed, maxJumpSpeed, minSpeed, fastAccel, accel, xDirection);

                        // Reset the Stuck_Jump_Timer after RunAtPlayer concludes
                        Stuck_Jump_Timer = 0;
                    }

                    Attack_Timer++;
                    AI_Timer++;
                }
            }
        }



        // WIP "spawn help" (in reality a thing that either despawns, or moves boss back to fight)
        /*
        public void SpawnHelp2(Player player)
        {
            float speed = 12f;
            float inertia = 20f;
        }
        */


        // Chooses a player to fight based off of distance, the closer the higher the chance of getting picked
        // The closest person gets DRASTICALLY higher priority
        public void choosePlayer()
        {
            double distanceBias = 75f;
            bool playerChosen = false;
            bool outOfPlayers = false;
            int likelyChosen = 1;

            NPC.TargetClosest();

            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];

                if (i != NPC.target && player.active)
                {
                    // if the current player is not the target and exists
                    double distance = FindDistance(NPC.position.X, player.position.X, NPC.position.Y, player.position.Y);

                    likelyChosen = 1 + (int)(distance / distanceBias);
                    playerChosen = Main.rand.NextBool(likelyChosen) ?  true : false;  
                }
                else if(!player.active)
                {
                    outOfPlayers = true;
                }

                if (playerChosen)
                {
                    NPC.target = i;
                    break;
                }
                else if(outOfPlayers)
                {
                    break;
                }
            }    
        }

        public bool SpawnHelp(Player player)
        {

            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.friendly = true;

            int yDirection = 0;
            int xDirection = 0;
            float maxSpeed = 8f;
            float accel = 0.3f;
            float maxBound = 200f;
            bool yReady = false;
            bool xReady = false;

            // In case it is beneath you
            if (NPC.position.Y > player.position.Y)
            {
                if (NPC.position.Y - player.position.Y < maxBound)
                {
                    yReady = true;
                }

                yDirection = -1;
            }
            // In case it is above you
            else if (player.position.Y > NPC.position.Y)
            {
                if (player.position.Y - NPC.position.Y < maxBound)
                {
                    yReady = true;
                }
                yDirection = 1;
            }
            // In case it is to the right of you
            if (NPC.position.X > player.position.X)
            {
                if (NPC.position.X - player.position.X < maxBound)
                {
                    xReady = true;
                }
                xDirection = -1;
            }
            // In case it is to the right of you
            else if (player.position.X > NPC.position.X)
            {
                if (player.position.X - NPC.position.X < maxBound)
                {
                    xReady = true;
                }
                xDirection = 1;
            }

            // If the enemy is within the bounds needed.
            if (yReady && xReady && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
                return true;
            }
            // If the enemy is out of bounds, help move it.
            else
            {
                // If x needs to move, then move
                if (Math.Abs(NPC.velocity.X) < maxSpeed)
                {
                    NPC.velocity.X += xDirection * accel;
                }
                else if (Math.Abs(NPC.velocity.X) >= maxSpeed)
                {
                    NPC.velocity.X = xDirection * maxSpeed;
                }

                // If y needs to move, then move
                if (Math.Abs(NPC.velocity.Y) < maxSpeed)
                {
                    NPC.velocity.Y += yDirection * accel;
                }
                else if (Math.Abs(NPC.velocity.Y) >= maxSpeed)
                {
                    NPC.velocity.Y = yDirection * maxSpeed;
                }

                return false;
            }
        }

        public bool CheckForDespawn(Player player)
        {
            if (!player.active || player.dead)
            {
                NPC.noTileCollide = true;
                if (NPC.velocity.Y < -1f)
                {
                    NPC.velocity.Y = -1f;
                }
                NPC.velocity.Y += 0.1f;

                if (NPC.velocity.Y > 12f)
                {
                    NPC.velocity.Y = 12f;
                }
                if (NPC.timeLeft > 90)
                {
                    NPC.timeLeft = 90;
                }
                if (AI_Timer != 0f)
                {
                    AI_Timer = 0f;
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        public void RunAtPlayer(Player player, float initialJump, float maxSpeed, float maxJumpSpeed, float minSpeed, float fastAccel, float accel, int xDirection)
        {

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
                float speed = 2.5f;
                float inertia = 50f;

                MoveHorizontal(speed, inertia, xDirection);
            }

            Stuck_Jump_Timer++;
        }

        public void JumpAtPlayer(Player player, float initialJump, float maxSpeed, float maxJumpSpeed, float minSpeed, float fastAccel, float accel, int xDirection)
        {

            if (Jump_Timer % 30 == 0 && jumping)
            {
                jumping = false;
            }

            if (Jump_Timer % 90 == 0)
            {
                NPC.velocity.Y += initialJump;
                jumping = true;
            }

            //float fastJumpAccel = fastAccel * 0.9f;
            //float minJumpSpeed = minSpeed + (maxJumpSpeed - maxSpeed);

            float speed = 2.5f;
            float inertia = 50f;

            MoveHorizontal(speed, inertia, xDirection);

            Jump_Timer++;
        }

        private const int jumpFrame1 = 23;
        private const int jumpFrame3 = 21;
        private const int jumpFrame4 = 20;
        private const int jumpFrame5 = 19;
        private const int jumpFrame6 = 18;
        private const int jumpFrame10 = 14;
        private const int jumpFrame11 = 13;
        private const int jumpFrame12 = 12;
        private const int jumpFrame13 = 11;

        private const int walkFrame1 = 9;
        private const int walkFrame2 = 8;
        private const int walkFrame3 = 7;
        private const int walkFrame4 = 6;
        private const int walkFrame5 = 5;
        private const int walkFrame6 = 4;
        private const int walkFrame7 = 3;
        private const int walkFrame8 = 2;
        private const int walkFrame9 = 1;
        private const int walkFrame10 = 0;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            bool balls = true;
            if (balls)
            {
                if (jumping)
                {
                    if (NPC.frameCounter < 5)
                    {
                        NPC.frame.Y = jumpFrame1 * frameHeight;
                    }
                    else if (NPC.frameCounter < 10)
                    {
                        NPC.frame.Y = jumpFrame3 * frameHeight;
                    }
                    else if (NPC.frameCounter < 15)
                    {
                        NPC.frame.Y = jumpFrame4 * frameHeight;
                    }
                    else if (NPC.frameCounter < 20)
                    {
                        NPC.frame.Y = jumpFrame5 * frameHeight;
                    }
                    else if (NPC.frameCounter < 30)
                    {
                        NPC.frame.Y = jumpFrame6 * frameHeight;
                    }
                    else if (NPC.frameCounter < 34)
                    {
                        NPC.frame.Y = jumpFrame10 * frameHeight;
                    }
                    else if (NPC.frameCounter < 38)
                    {
                        NPC.frame.Y = jumpFrame11 * frameHeight;
                    }
                    else if (NPC.frameCounter < 42)
                    {
                        NPC.frame.Y = jumpFrame12 * frameHeight;
                    }
                    else if (NPC.frameCounter < 46)
                    {
                        NPC.frame.Y = jumpFrame13 * frameHeight;
                    }
                    else
                    {
                        NPC.frameCounter = 0;
                    }
                }
                else
                {
                    if (NPC.frameCounter < 7)
                    {
                        NPC.frame.Y = walkFrame10 * frameHeight;
                    }
                    else if (NPC.frameCounter < 14)
                    {
                        NPC.frame.Y = walkFrame9 * frameHeight;
                    }
                    else if (NPC.frameCounter < 21)
                    {
                        NPC.frame.Y = walkFrame8 * frameHeight;
                    }
                    else if (NPC.frameCounter < 28)
                    {
                        NPC.frame.Y = walkFrame7 * frameHeight;
                    }
                    else if (NPC.frameCounter < 35)
                    {
                        NPC.frame.Y = walkFrame6 * frameHeight;
                    }
                    else if (NPC.frameCounter < 42)
                    {
                        NPC.frame.Y = walkFrame5 * frameHeight;
                    }
                    else if (NPC.frameCounter < 49)
                    {
                        NPC.frame.Y = walkFrame4 * frameHeight;
                    }
                    else if (NPC.frameCounter < 56)
                    {
                        NPC.frame.Y = walkFrame3 * frameHeight;
                    }
                    else if (NPC.frameCounter < 63)
                    {
                        NPC.frame.Y = walkFrame2 * frameHeight;
                    }
                    else if (NPC.frameCounter < 70)
                    {
                        NPC.frame.Y = walkFrame1 * frameHeight;
                    }
                    else
                    {
                        NPC.frameCounter = 0;
                    }
                }

            }
            else
            {
                NPC.frame.Y = walkFrame1 * frameHeight;
            }
            
        }
    }
}
