using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier1;
using OmoriMod.Projectiles.Friendly.Melee.Knife;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier2
{
    public class CrimsonKnife : SadItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // clone default weapon stuff
            Item.CloneDefaults(ModContent.ItemType<CorruptionBat>());

            // change projectile
            Item.shoot = ModContent.ProjectileType<KnifeProj>();

            // sad item
            SetSadDefaults();
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<Knife>(), 1);
            recipe1.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
    }
}