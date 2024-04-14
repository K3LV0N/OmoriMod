using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Projectiles.Friendly.BossRelated.YeOldSprout;

namespace OmoriMod.Items.BossRelated.YeOldSproutWeapons
{
    public class SproutShotgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            // damage
            Item.damage = 12;
            Item.knockBack = 6;

            // size
            Item.scale = 0.5f;
            
            // ranged weapon stuff
            int useSpeed = 20;
            float shotVelocity = 8f;
            Item.DefaultToRangedWeapon(ModContent.ProjectileType<SproutBulletProj>(),
                ModContent.ItemType<SproutBullet>(), useSpeed, shotVelocity, true);

            // usage
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            // rarity
            Item.rare = ItemRarityID.Purple;

            // price
            Item.value = Item.buyPrice(platinum: 0, gold: 4, silver: 50, copper: 0);
        }

        public override Vector2? HoldoutOffset()
        {

            Vector2 offset = new Vector2(-35, 0);
            return offset;

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            int extraProjectiles = 4;
            int minAngle = -10;
            int maxAngle = 11;
            Vector2 startingVelocity = velocity;


            Random rand = new Random();

            HashSet<int> randomAngles = new HashSet<int>();
            randomAngles.Add(0);


            for (int i = 0; i < extraProjectiles; i++)
            {
                int randNumber = rand.Next(minAngle, maxAngle);
                while (!randomAngles.Add(randNumber))
                {
                    randNumber = rand.Next(minAngle, maxAngle);
                }
                
            }

            foreach (int randomAngle in randomAngles)
            {
                if (randomAngle != 0)
                {
                    Vector2 proj = startingVelocity;
                    float angle = MathHelper.ToRadians(randomAngle);
                    Matrix matrix = Matrix.CreateRotationZ(angle);

                    proj = Vector2.Transform(proj, matrix);
                    Projectile.NewProjectile(Item.GetSource_NaturalSpawn(), position, proj, ModContent.ProjectileType<SproutBulletProj>(), damage, knockback);
                }
                
            }

            return true;
        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            float time = 2.1f;
            position = position + (velocity * time);
        }
    }

}
