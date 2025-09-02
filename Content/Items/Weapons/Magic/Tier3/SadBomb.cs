using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Items.Weapons.Magic.Tier2;
using OmoriMod.Content.Projectiles.Friendly.Magic.Tier3;

namespace OmoriMod.Content.Items.Weapons.Magic.Tier3
{
    public class SadBomb : SadItem
    {
        SadBomb()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBomb>(ModContent.ProjectileType<SadBombProjectile>());
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<SadBundle>(),
                extraItemID: ItemID.HallowedBar,
                extraItemAmount: 20,
                craftingStationID: TileID.Bookcases
                );
        }
    }
}
