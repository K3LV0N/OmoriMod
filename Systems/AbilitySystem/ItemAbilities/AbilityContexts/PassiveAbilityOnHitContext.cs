using Terraria;

namespace OmoriMod.Systems.AbilitySystem.ItemAbilities.AbilityContexts
{
    public class PassiveAbilityOnHitContext : AbilityContext
    {
        public NPC Target { get; set; }
        public int DamageDone { get; set; }
        public float Knockback { get; set; }
        public bool Crit { get; set; }

        public PassiveAbilityOnHitContext(Player player, Item item, NPC target, int damageDone, float knockback, bool crit) 
            : base(player, item)
        {
            Target = target;
            DamageDone = damageDone;
            Knockback = knockback;
            Crit = crit;
        }
    }
}
