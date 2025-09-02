﻿using OmoriMod.Content.Items.Ammo.Arrows.Regular.Tier2;
using OmoriMod.Content.Projectiles.Friendly.Arrows.Tier1.NoDrops;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Content.Projectiles.Friendly.Arrows.Tier2.CanDrop
{
    public class HappyArrowPlusProjectile : HappyProjectile
    {
        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillWithDrop<HappyArrowPlus>(timeLeft);
        }

        public override bool PreAI()
        {
            MakeDust();
            return true;
        }

        public override void AI()
        {
            AI_MultiSplittingProjectile<HappyArrowProjectileNoDropNoDust>(maxAngle: 32, ProjectileAmount: 9);
        }
    }
}
