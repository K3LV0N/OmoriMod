using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Melee.Bat;

namespace OmoriMod.Projectiles.Friendly.Melee.Knife
{
    public class KnifeProjSeeking : SadProj
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<BatProj>());
        }

        public override void OnKill(int timeleft)
        {
            OnKillNoDrop(timeleft, noSound: true);
            DustTrail();
        }

        public override void AI()
        {
            AI_SeekingScytheProjectile(ticksStationaryUntilDespawn: 60, rotation: 0.5f, seekingDistance: 300);
        }
    }
}
