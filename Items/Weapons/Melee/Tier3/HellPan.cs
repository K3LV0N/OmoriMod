using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier2;
using OmoriMod.Projectiles.Friendly.Melee.Pan;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier3
{
    public class HellPan : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<HellBat>(ModContent.ProjectileType<PanProjectileTriple>());
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<CorruptionPan>(),
                extraItemID: ItemID.HellstoneBar,
                extraItemAmount: 15,
                craftingStationID: TileID.Anvils
                );
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<CrimsonPan>(),
                extraItemID: ItemID.HellstoneBar,
                extraItemAmount: 15,
                craftingStationID: TileID.Anvils
                );
        }
    }
}
