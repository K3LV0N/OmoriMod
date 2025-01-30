using Microsoft.Xna.Framework;
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
            EmotionalItemCloneWithDifferentProjectile<ChlorBat>(ModContent.ProjectileType<KnifeProjFiveSeeking>());
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
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