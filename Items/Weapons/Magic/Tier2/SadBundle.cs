using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.Magic.Tier2;
using OmoriMod.Items.Weapons.Magic.Tier1;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.Weapons.Magic.Tier2
{
    public class SadBundle : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBundle>(ModContent.ProjectileType<SadBundleProjectile>());
        }

        public override void AddRecipes()
        {
            MakeUpgradeRecipe(
                baseItemID: ModContent.ItemType<SadBolt>(),
                extraItemID: ItemID.HellstoneBar,
                extraItemAmount: 15,
                craftingStationID: TileID.Bookcases
                );
        }
    }
}
