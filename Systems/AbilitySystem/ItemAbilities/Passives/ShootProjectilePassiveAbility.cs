using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Systems.AbilitySystem.ItemAbilities.Passives
{
    public class ShootProjectilePassiveAbility : IPassiveAbility
    {
        private int _projectileType;
        public int ProjectileType { get => _projectileType; set => _projectileType = value; }

        public ShootProjectilePassiveAbility(int projectileType) {
            ProjectileType = projectileType;
        }

        public bool IsUnlocked(Item item, Player player)
        {
            return false;
        }

        public bool IsEquippable(Item item, Player player)
        {
            return false;
        }

        private bool MoveProjectileForward(ref Vector2 position, ref Vector2 velocity, float ticks)
        {
            Projectile projectile = ProjectileLoader.GetProjectile(_projectileType).Projectile;

            int actingTicks = (int)MathF.Floor(ticks);
            Vector2 actingVelocity = velocity;

            while (ticks % 1 != 0)
            {
                actingTicks *= 10;
                ticks *= 10;
                actingVelocity /= 10;
            }

            for (int i = 0; i < actingTicks; i++)
            {
                // compute next canidate position
                Vector2 nextPos = position + actingVelocity;
                var hitbox = new Rectangle(
                    (int)nextPos.X,
                    (int)nextPos.Y,
                    projectile.width,
                    projectile.height
                );

                // If that spot collides with solid tiles, abort early
                if (Collision.SolidCollision(hitbox.TopLeft(), hitbox.Width, hitbox.Height))
                {
                    return false;
                }

                position = nextPos;
            }

            return true;
        }

        /// <summary>
        /// Call inside of <see cref="ModItem.Shoot(Player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo, Vector2, Vector2, int, int, float)"/>.
        /// This method expects a signature like 
        /// <see cref="ModItem.ModifyShootStats(Player, ref Vector2, ref Vector2, ref int, ref int, ref float)"/> with an <see cref="Item"/> object at the end.
        /// </summary>
        /// <param name="args">
        /// <see cref="Player"/> <paramref name="player"/>, 
        /// ref <see cref="Vector2"/> <paramref name="position"/>, 
        /// ref <see cref="Vector2"/> <paramref name="velocity"/>, 
        /// ref <see cref="int"/> <paramref name="type"/>,
        /// ref <see cref="int"/> <paramref name="damage"/>, 
        /// ref <see cref="float"/> <paramref name="knockback"/>, 
        /// <see cref="Item"/> <paramref name="item"/>,
        /// <see cref="float"/> <paramref name="ticksToMoveProjectileForward"/>
        /// </param>
        /// <returns>false</returns>
        public bool PerformAbility(params object[] args)
        {
            Player player = (Player)args[0];
            Vector2 position = (Vector2)args[1];
            Vector2 velocity = (Vector2)args[2];
            int type = (int)args[3];
            int damage = (int)args[4];
            float knockback = (float)args[5];
            Item item = (Item)args[6];

            float ticksToMoveFoward = 0f;
            if (args.Length > 7) { ticksToMoveFoward = (float)args[7]; }

            type = ProjectileType;

            MoveProjectileForward(ref position, ref velocity, ticksToMoveFoward);

            return ShootProjectile(player, ref position, ref velocity, ref type, ref damage, ref knockback, item);
        }
        private static bool ShootProjectile(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback, Item item)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                string context = "ShootProjectileActiveAbility:ShootProjectile";
                Projectile.NewProjectile(player.GetSource_ItemUse(item, context), position, velocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
