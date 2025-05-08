using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier1;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier2
{
    public class BundledAnger : AngryProjectile
    {
        public override void SetDefaults()
        {
            SetOtherDefaults(width: 46, height: 42, damageType: DamageClass.Magic, aiStyle: -1, penetration: -1, scale: 1, tileCollide: true);
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft, noSound: true);
        }

        public override void AI()
        {
            AI_BundledProjectile<AngryBolt>(damagePerProjectile: 18, ProjectileSpeedOnSpawn: 6, volleys: 4, shotsPerVolley: 5, interval: 60);
        }
    }
}
