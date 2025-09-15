using Microsoft.Xna.Framework;
using OmoriMod.Content.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Content.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Items.Weapons.Melee.Tier1;
using OmoriMod.Content.Projectiles.Friendly.Melee.Knife;

namespace OmoriMod.Content.Items.Weapons.Melee.Tier2
{
    public class CrimsonKnife : SadItem
    {
        CrimsonKnife()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<CorruptionBat>(ModContent.ProjectileType<KnifeProjectile>());
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, ref type, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<Knife>(),
                extraItemID: ItemID.CrimtaneBar,
                extraItemAmount: 10,
                craftingStationID: TileID.Anvils
                );
        }
    }
}