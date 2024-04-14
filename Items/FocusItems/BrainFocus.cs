using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.DamageClasses;
using OmoriMod.Projectiles.Friendly.FocusProjectiles;
using OmoriMod.Items.BuffItems;
using OmoriMod.Items.Abstract_Classes;

namespace OmoriMod.Items.FocusItems
{
    public class BrainFocus : FocusClass
    {
        public override void SetDefaults()
        { 
            charge = 0;
            dps = 2;
            maxCharge = 7 * 60;
            timer = 0;
            chargeTimer = 0;
            decayTimer = 0;
            timeUntilChargeStarts = 40;
            timeUntilDecayStarts = 20;
            decayRate = 2;
            charging = false;
            decaying = true;

            // damage
            Item.damage = 3;
            Item.knockBack = 6;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<FocusDamage>();

            // size
            Item.height = 32;
            Item.width = 26;

            // projectiles
            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<BrainBolt>();

            // usage
            Item.useTime = 7;
            Item.useAnimation = Item.useTime;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;

            // rarity
            Item.rare = ItemRarityID.Blue;

            // price
            Item.value = Item.buyPrice(platinum: 0, gold: 1, silver: 0, copper: 0);
        }

        public override void HoldItem(Player player)
        {
            HoldItemFocus(player);
        }

        public override void UpdateInventory(Player player)
        {
            UpdateInventoryFocus(player);
        } 

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient<RainCloud>(10);
            recipe.Register();  
        }
    }
}
