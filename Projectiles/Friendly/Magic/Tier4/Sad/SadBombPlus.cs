using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier4.Sad
{
    public class SadBombPlus : SadProj
    {
        public override void SetDefaults()
        {
            SetOtherDefaults(width: 24, height: 24, damageType: DamageClass.Magic, aiStyle: 0, penetration: 5, scale: 1, tileCollide: true);
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }

        public override bool PreAI()
        {
            DustTrail();
            return true;
        }

        public override void AI()
        {
            AI_MagicBombProjectileWithFlip<BundledSadnessNoTrail>(damagePerProjectile: 32, projectileSpeedOnSpawn: 6, volleys: 6, shotsPerVolley: 4, interval: 30);
        }
    }
}
