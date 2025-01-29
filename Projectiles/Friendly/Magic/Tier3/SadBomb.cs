using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier1;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier3
{
    public class SadBomb : SadProj
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
            AI_MagicBombProjectile<SadBolt>(damagePerProjectile: 18, projectileSpeedOnSpawn: 6, volleys: 6, shotsPerVolley: 8, interval: 30);
        }
    }
}
