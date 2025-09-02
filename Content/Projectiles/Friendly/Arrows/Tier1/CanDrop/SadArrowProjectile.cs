﻿using OmoriMod.Content.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Content.Projectiles.Friendly.Arrows.Tier1.CanDrop
{
    public class SadArrowProjectile : SadProjectile
    {
        public override void SetDefaults()
        {
            SetArrowDefaults();
        }

        public override void OnKill(int timeLeft)
        {
            OnKillWithDrop<SadArrow>(timeLeft);
        }

        public override bool PreAI()
        {
            MakeDust();
            return true;
        }
    }
}
