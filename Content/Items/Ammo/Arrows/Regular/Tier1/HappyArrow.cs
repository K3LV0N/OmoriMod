using OmoriMod.Items.Abstract_Classes;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Items.BuffItems;
using OmoriMod.Content.Projectiles.Friendly.Arrows.Tier1.CanDrop;

namespace OmoriMod.Content.Items.Ammo.Arrows.Regular.Tier1
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
