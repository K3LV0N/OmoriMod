using Terraria.ID;
using OmoriMod.Items.Abstract_Classes;
using System.Collections.Generic;

namespace OmoriMod.Items.Weapons.Melee.Tier1
{
    public class Knife : SadItem
    {
        Knife()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
        }
        public override void SetDefaults()
        {
            EmotionItemClone<Bat>();
        }

        public override void AddRecipes()
        {
            var recipes = new List<(int, int)> {
                (ItemID.IronBar, 6),
                (ItemID.LeadBar, 6)
            };
            MakeRegularRecipes(
                ingredients: recipes,
                craftingStationID: TileID.Anvils
                );
        }
    }
}