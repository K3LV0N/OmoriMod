using Microsoft.Xna.Framework;
using OmoriMod.Content.Items.Abstract_Classes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Content.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Items.Weapons.Melee.Tier2;
using OmoriMod.Content.Projectiles.Friendly.Melee.Knife;

using OmoriMod.Systems.AbilitySystem.ItemAbilities.Registries;

namespace OmoriMod.Content.Items.Weapons.Melee.Tier3
{
    public class HellKnife : SadItem
    {
        HellKnife()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            InnatePassiveAbilityID = PassiveAbilityRegistry.PassiveAbilityID.TRIPLE_PHANTOM_KNIFE;
            EmotionItemCloneWithDifferentProjectile<HellBat>(ModContent.ProjectileType<KnifeProjectileTriple>());
        }



        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<CorruptionKnife>(),
                extraItemID: ItemID.HellstoneBar,
                extraItemAmount: 15,
                craftingStationID: TileID.Anvils
                );
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<CrimsonKnife>(),
                extraItemID: ItemID.HellstoneBar,
                extraItemAmount: 15,
                craftingStationID: TileID.Anvils
                );
        }
    }
}