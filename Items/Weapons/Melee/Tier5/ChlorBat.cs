using Microsoft.Xna.Framework;
using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Weapons.Melee.Tier4;
using OmoriMod.Projectiles.Friendly.Melee.Bat;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.Weapons.Melee.Tier5
{
    public class ChlorBat : AngryItem
    {
        public override void SetDefaults()
        {
            Item.ResearchUnlockCount = 1;

            // damage
            Item.damage = 70;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee;

            // projectiles
            Item.shoot = ModContent.ProjectileType<BatProjFiveSeeking>();
            Item.shootSpeed = 8f;

            // size
            Item.scale = 1.5f;
            Item.width = (int)(32 * Item.scale);
            Item.height = (int)(32 * Item.scale);

            // usage
            Item.useTime = 12;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            // price
            Item.value = Item.buyPrice(0, 16, 0, 0);

            // angry item
            SetAngryDefaults();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            MoveProjectileForward(ref position, ref velocity, meleeWeaponProjectileMoveTime);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HallowBat>(), 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
