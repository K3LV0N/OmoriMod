using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier4.Happy
{
    public class BundledHappinessNoTrail : HappyProj
    {
        public override void SetDefaults()
        {
            SetOtherDefaults(width: 46, height: 42, damageType: DamageClass.Magic, aiStyle: -1, penetration: int.MaxValue, scale: 1, tileCollide: true);
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft, noSound: true);
        }

        public override void AI()
        {
            AI_TravelingBundleProjectile<HappyBoltNoTrail>(damagePerProjectile: 18, projectileSpeedOnSpawn: 6, volleys: 4, interval: 60);
        }
    }
}
