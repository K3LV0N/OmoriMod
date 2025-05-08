using OmoriMod.Projectiles.Friendly.BossRelated.YeOldSprout;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.BossRelated.YeOldSproutWeapons
{
    public class SproutScythe : ModItem
    {
        public override void SetDefaults()
        {
            // default stuff
            int shotTime = 25;
            float velocity = 20f;
            bool autoReuse = true;
            Item.DefaultToMagicWeapon(ModContent.ProjectileType<SproutScytheProjectile>(), shotTime, velocity, autoReuse);


            // damage
            Item.damage = 20;
            Item.mana = 10;

            // usage
            Item.UseSound = SoundID.Item1;

            // rarity
            Item.rare = ItemRarityID.Purple;

            // price
            Item.value = Item.buyPrice(platinum: 0, gold: 4, silver: 50, copper: 0);

        }
    }
}
