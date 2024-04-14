using Microsoft.Xna.Framework;
using Terraria;

namespace OmoriMod.Projectiles.Abstract_Classes.SetSpeedProj
{
    // Mainly used for projectiles I want to speed up
    public abstract class SetSpeedProj : EmotionProj
    {
        public virtual void AI_SetSpeedProj(float totalSpeed, float gravGiven, bool gravity)
        {

            Projectile.velocity.Normalize();
            Projectile.velocity *= totalSpeed;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;


            // 0.1f for normal arrow gravity, 0.4f for knife gravity
            // float arrowGrav = 0.1f;
            // float knifeGrav = 0.4f;
            // float defaultTerminal = 16f;

            if (gravity)
            {
                TheGravityOfTheSituation(gravGiven, totalSpeed);
            }

            
        }      

        public virtual void TheGravityOfTheSituation(float grav, float terminalVelocity)
        {
            if (Projectile.velocity.Y > terminalVelocity)
                Projectile.velocity.Y = terminalVelocity;

            if (Projectile.velocity.Y < terminalVelocity)
                Projectile.velocity.Y += grav;
        }

    }
}
