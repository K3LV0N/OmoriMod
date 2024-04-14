using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Projectiles.Abstract_Classes;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Melee.Pan
{
    public class PanProj : HappyProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = 0;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.timeLeft = 300;

            Projectile.alpha = 50;

            Projectile.arrow = false;
        }

        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }


        public bool AI_TimerStartX = false;
        public bool AI_TimerStartY = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(AI_TimerStartX);
            writer.Write(AI_TimerStartY);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            AI_TimerStartX = reader.ReadBoolean();
            AI_TimerStartY = reader.ReadBoolean();
        }
        public override void AI()
        {
            Projectile.timeLeft--;
            if (Projectile.timeLeft <= 0)
            {
                Projectile.Kill();
            }

            if (Projectile.direction > 0)
            {
                Projectile.rotation += .5f;
            }
            else
            {
                Projectile.rotation -= .5f;
            }

            if (Math.Abs(Projectile.velocity.X) > .5)
            {
                Projectile.velocity = new Vector2(Projectile.velocity.X * .97f, Projectile.velocity.Y);
            }
            else
            {
                Projectile.velocity = new Vector2(0, Projectile.velocity.Y);
                AI_TimerStartX = true;
            }

            if (Math.Abs(Projectile.velocity.Y) > .5)
            {
                Projectile.velocity = new Vector2(Projectile.velocity.X, Projectile.velocity.Y * .97f);
            }
            else
            {
                Projectile.velocity = new Vector2(Projectile.velocity.X, 0);
                AI_TimerStartY = true;
            }


            if (AI_TimerStartX && AI_TimerStartY)
            {
                AI_Timer++;

                if (AI_Timer > 60)
                {
                    Projectile.Kill();
                }
            }


        }

        public override void OnKill(int timeleft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Yellow);
        }

    }
}
