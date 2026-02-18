using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace OmoriMod.Systems.AbilitySystem.ItemAbilities.AbilityContexts
{
    public class PassiveAbilityShootContext : AbilityContext
    {
        public EntitySource_ItemUse_WithAmmo Source { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int Type { get; set; }
        public int Damage { get; set; }
        public float Knockback { get; set; }
        public float? TicksToMoveForward { get; set; }

        public PassiveAbilityShootContext(Player player, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float? ticksToMoveForward = null) 
            : base(player, item)
        {
            Source = source;
            Position = position;
            Velocity = velocity;
            Type = type;
            Damage = damage;
            Knockback = knockback;
            TicksToMoveForward = ticksToMoveForward;
        }
    }
}
