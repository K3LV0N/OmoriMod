using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier1;
using OmoriMod.Items.BuffItems;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier1
{
    public class HappyBolt : HappyItem
    {
        HappyBolt()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBolt>(ModContent.ProjectileType<HappyBoltProjectile>());
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ItemID.Book,
                extraItemID: ModContent.ItemType<PartyPopper>(),
                extraItemAmount: 10,
                craftingStationID: TileID.Bookcases
                );
        }
    }
}
