﻿using Microsoft.Xna.Framework;
using OmoriMod.Summons.Pets.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Summons.Pets.Buffs
{
    internal class SomethingBuff : ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 69420;
            int projType = ModContent.ProjectileType<SomethingProjectile>();

            // If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
            {
                var entitySource = player.GetSource_Buff(buffIndex);

                Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
            }
        }
    }
}