using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier4;
using OmoriMod.Projectiles.Friendly.Melee.Pan;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier5
{
    public class ChlorPan : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<ChlorBat>(ModContent.ProjectileType<PanProjectileFiveSeeking>());
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<HallowPan>(),
                extraItemID: ItemID.ChlorophyteBar,
                extraItemAmount: 25,
                craftingStationID: TileID.MythrilAnvil
                );
        }
    }
}
