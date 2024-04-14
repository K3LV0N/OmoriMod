using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier2;
using OmoriMod.Projectiles.Friendly.Melee.Knife;
using OmoriMod.Projectiles.Friendly.Melee.Pan;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier3
{
    public class HellKnife : SadItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // clone default weapon stuff
            Item.CloneDefaults(ModContent.ItemType<HellBat>());

            // change projectile
            Item.shoot = ModContent.ProjectileType<KnifeProjTriple>();

            // sad item
            SetSadDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<CorruptionKnife>(), 1);
            recipe1.AddIngredient(ItemID.HellstoneBar, 15);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<CrimsonKnife>(), 1);
            recipe2.AddIngredient(ItemID.HellstoneBar, 15);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}