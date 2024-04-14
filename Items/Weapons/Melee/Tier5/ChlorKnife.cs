using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier4;
using OmoriMod.Projectiles.Friendly.Melee.Knife;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier5
{
    public class ChlorKnife : SadItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // clone default weapon stuff
            Item.CloneDefaults(ModContent.ItemType<ChlorBat>());

            // change projectile
            Item.shoot = ModContent.ProjectileType<KnifeProjFiveSeeking>();

            // sad item
            SetSadDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<HallowKnife>(), 1);
            recipe1.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe1.AddTile(TileID.MythrilAnvil);
            recipe1.Register();
        }
    }
}