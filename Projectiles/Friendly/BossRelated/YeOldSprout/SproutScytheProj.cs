using Terraria;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;


namespace OmoriMod.Projectiles.Friendly.BossRelated.YeOldSprout
{
    public class SproutScytheProj : EmotionProj
    {
        public override void SetDefaults()
        {
            // Set magic projectile defaults
            SetOtherDefaults(width: 32, height: 32, damageType: DamageClass.Magic, aiStyle: 0, scale: 1.2f);

            // Time and penetration ;)
            Projectile.timeLeft = 365;
            Projectile.penetrate = 3;
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }

        public override void AI()
        {
            AI_ScytheProjectile(ticksStationaryUntilDespawn: 60, rotation: 0.7f);
        }
    }
}
