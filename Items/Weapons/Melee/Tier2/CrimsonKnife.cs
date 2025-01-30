using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier1;
using OmoriMod.Projectiles.Friendly.Melee.Knife;
using OmoriMod.Projectiles.Friendly.Melee.Pan;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier2
{
    public class CrimsonKnife : SadItem
    {
        public override void SetDefaults()
        {
            EmotionalItemCloneWithDifferentProjectile<CorruptionBat>(ModContent.ProjectileType<KnifeProj>());
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
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