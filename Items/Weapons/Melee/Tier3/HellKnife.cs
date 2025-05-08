using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier2;
using OmoriMod.Projectiles.Friendly.Melee.Knife;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier3
{
    public class HellKnife : SadItem
    {
        public override void SetDefaults()
        {
            EmotionItemCloneWithDifferentProjectile<HellBat>(ModContent.ProjectileType<KnifeProjectileTriple>());
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
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