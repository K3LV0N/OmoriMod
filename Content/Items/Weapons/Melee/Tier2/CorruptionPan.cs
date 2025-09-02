using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Items.Weapons.Melee.Tier1;
using OmoriMod.Content.Projectiles.Friendly.Melee.Pan;

namespace OmoriMod.Content.Items.Weapons.Melee.Tier2
{
    public class CorruptionPan : HappyItem
    {
        CorruptionPan()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<CorruptionBat>(ModContent.ProjectileType<PanProjectile>());
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<FryingPan>(),
                extraItemID: ItemID.DemoniteBar,
                extraItemAmount: 10,
                craftingStationID: TileID.Anvils
                );
        }
    }
}
