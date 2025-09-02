using OmoriMod.Content.Projectiles.Friendly.Melee.Bat;
using Terraria.ModLoader;

namespace OmoriMod.Content.Players
{

    /// <summary>
    /// Keeps track of ability related things. Such as whether the ability menu is up or not
    /// </summary>
    public class AbilityPlayer : ModPlayer
    {
        public bool abilityMenuActive;
        public int projectileID;

        AbilityPlayer()
        {
            abilityMenuActive = false;
            projectileID = ModContent.ProjectileType<BatProjectile>();
        }
    }
}
