using Terraria;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.NoDrops;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier2.CanDrop
{
    public class HappyArrowPlusProj : HappyProj
    {

        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override bool PreAI()
        {
            DustTrail();
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            OnKillWithDrop();
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
    }
}
