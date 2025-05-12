using Terraria.ModLoader;
using OmoriMod.Projectiles.Abstract_Classes;

namespace OmoriMod.Projectiles.Friendly.Magic.Tier1
{
    public class AngryBoltProjectile : AngryProjectile
    {
        public override void SetDefaults()
        {
            SetOtherDefaults(width: 8, height: 8, damageType: DamageClass.Magic, aiStyle: 1, penetration: 1, scale: 1, tileCollide: true);
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
