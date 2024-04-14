using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OmoriMod.Dusts;
using OmoriMod.Projectiles.Abstract_Classes;
using OmoriMod.Items.Ammo.Arrows.Regular.Tier1;

namespace OmoriMod.Projectiles.Friendly.Arrows.Tier1.CanDrop
{
    public class AngryArrowProj : AngryProj
    {

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 12;

            Projectile.aiStyle = ProjAIStyleID.Arrow;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 14;

            Projectile.arrow = true;
        }

        public override bool PreAI()
        {
            Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<EmotionDust>(), 0f, 0f, 0, Color.Red);
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            if (Projectile.owner == Main.myPlayer)
            {
                //has a chance to drop arrow for pickup
                int item = Main.rand.NextBool(5) ? Item.NewItem(Entity.GetSource_Death(), Projectile.getRect(), ModContent.ItemType<AngryArrow>()) : 0;
            }
        }
    }
}
