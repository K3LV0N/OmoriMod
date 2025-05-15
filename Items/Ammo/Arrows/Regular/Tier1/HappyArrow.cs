using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.BuffItems;
using OmoriMod.Projectiles.Friendly.Arrows.Tier1.CanDrop;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Ammo.Arrows.Regular.Tier1
{
    public class HappyArrow : HappyItem
    {
        HappyArrow()
        {
            itemTypeForResearch = ItemTypeForResearch.Ammo_Explosives;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryArrow>(ModContent.ProjectileType<HappyArrowProjectile>());
        }

        public override void AddRecipes()
        {
            // Create recipes
            MakeAmmoRecipes(
                resultAmount: 50,

                baseIngredientID: ModContent.ItemType<PartyPopper>(),
                baseAmount: 1,

                nonEndlessIngredientID: ItemID.WoodenArrow,
                nonEndlessAmount: 50,

                endlessIngredientID: ItemID.EndlessQuiver
                );
        }
    }
}
