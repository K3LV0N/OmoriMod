﻿using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop
{
    public class HappyArrowPlusProj : HappyProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;

            Projectile.aiStyle = ProjAIStyleID.Arrow;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 24;

            Projectile.arrow = true;
        }

        public override bool PreAI()
        {
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Yellow);
            return true;
        }


        public float AI_Timer
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void AI()
        {
            float deathTime = 20;
            if (AI_Timer == 10 || AI_Timer == deathTime)
            {
                float speed = Projectile.velocity.Length();
                int maxAngle = 32;
                int projectileAmount = 9;
                SetSplit<HappyArrowProjNoDropOrTrail>(projectileAmount, Projectile.damage, maxAngle, speed, Projectile.knockBack, false);
            }
            AI_Timer++;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            if (Projectile.owner == Main.myPlayer)
            {
                //has a chance to drop arrow for pickup
                int item = Main.rand.NextBool(5) ? Item.NewItem(Entity.GetSource_Death(), Projectile.getRect(), ModContent.ItemType<HappyArrowPlus>()) : 0;
            }
        }
    }
}
