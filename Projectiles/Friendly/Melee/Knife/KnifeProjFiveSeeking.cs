using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Melee.Pan;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Friendly.Melee.Knife
{
    public class KnifeProjFiveSeeking : SadProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = 0;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.alpha = 50;

            Projectile.arrow = false;
        }

        public float AI_Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public override void AI()
        {
            if (AI_Timer == 0)
            {
                float speed = Projectile.velocity.Length();
                int maxAngle = 20;
                int projectileAmount = 5;
                SetSplit<KnifeProjSeeking>(projectileAmount, Projectile.damage, maxAngle, speed, Projectile.knockBack);
            }
            AI_Timer++;


        }

        public override void OnKill(int timeleft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Blue);
        }

    }
}
