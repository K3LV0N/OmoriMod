﻿using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Content.Projectiles.Friendly.BossRelated.YeOldSprout
{
    public class SproutBulletProjectile : OmoriModProjectile
    {
        public override void SetDefaults()
        {
            // Set bullet Projectile defaults
            SetBulletDefaults(width: 8, height: 8);
        }

        public override bool PreAI()
        {   
            VelocityRotateWith90(flip: false);
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }
    }
}
