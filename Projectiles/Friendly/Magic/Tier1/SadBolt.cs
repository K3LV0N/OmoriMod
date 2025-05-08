using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier1
{
    public class SadBolt : SadProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<AngryBolt>());
        }

        public override void OnKill(int timeLeft)
        {
            OnKillNoDrop(timeLeft);
        }

        public override bool PreAI()
        {
            MakeDust();
            return true;
        }
    }
}
