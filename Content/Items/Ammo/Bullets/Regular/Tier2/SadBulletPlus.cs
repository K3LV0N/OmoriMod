using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Abstract_Classes.BaseClasses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Content.Projectiles.Friendly.Bullets.Tier2;
using OmoriMod.Content.Items.Ammo.Bullets.Regular.Tier1;
using OmoriMod.Content.Items.Ammo.Bullets.Unlimited.Tier1;

namespace OmoriMod.Content.Items.Ammo.Bullets.Regular.Tier2
{
    public class SadBulletPlus : SadItem
    {
        SadBulletPlus()
        {
            itemTypeForResearch = ItemTypeForResearch.Ammo_Explosives;
        }
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<AngryBulletPlus>(ModContent.ProjectileType<SadBulletPlusProjectile>());
            Item.damage = 50;
            Item.shootSpeed = 30f;
        }

        public override void AddRecipes()
        {
            // Create recipes
            MakeAmmoRecipes(
                resultAmount: 100,

                baseIngredientID: ItemID.HallowedBar,
                baseAmount: 1,

                nonEndlessIngredientID: ModContent.ItemType<SadBullet>(),
                nonEndlessAmount: 100,

                endlessIngredientID: ModContent.ItemType<InfiniteSadBullet>()
                );
        }
    }
}
