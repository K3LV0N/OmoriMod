using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier3;
using OmoriMod.Projectiles.Friendly.Melee.Knife;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier4
{
    public class HallowKnife : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<HallowBat>(ModContent.ProjectileType<KnifeProjectileFive>());
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<HellKnife>(),
                extraItemID: ItemID.HallowedBar,
                extraItemAmount: 20,
                craftingStationID: TileID.MythrilAnvil
                );
        }
    }
}