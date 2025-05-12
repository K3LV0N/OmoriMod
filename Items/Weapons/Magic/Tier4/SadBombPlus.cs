using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Weapons.Magic.Tier3;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Projectiles.Friendly.Magic.Tier4;

namespace OmoriMod.Items.Weapons.Magic.Tier4
{
    public class SadBombPlus : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBombPlus>(ModContent.ProjectileType<SadBombPlusProjectile>());
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<SadBomb>(),
                extraItemID: ItemID.ChlorophyteBar,
                extraItemAmount: 25,
                craftingStationID: TileID.Bookcases
                );
        }
    }
}
