﻿using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier1.CanDrop
{
    public class SadArrowProj : SadProj
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
            DustTrail();
            return true;
        }
    }
}
