using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier1
{
    public class SadBoltNoTrail : SadProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<AngryBolt>());
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }
    }
}
