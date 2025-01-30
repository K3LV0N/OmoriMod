using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier1;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier2
{
    public class CrimsonBat : AngryItem
    {
        public override void SetDefaults()
        {
            EmotionalItemClone<CorruptionBat>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Bat>(), 1);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
