using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier3;
using OmoriMod.Items.Weapons.Magic.Tier2;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier3
{
    public class HappyBomb : HappyItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBomb>(ModContent.ProjectileType<HappyBombProjectile>());
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<HappyBundle>(),
                extraItemID: ItemID.HallowedBar,
                extraItemAmount: 20,
                craftingStationID: TileID.Bookcases
                );
        }
    }
}
