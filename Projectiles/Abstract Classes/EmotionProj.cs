using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Projectiles.Abstract_Classes
{
    public abstract class EmotionProj : ModProjectile
    {
        public enum emotionType
        {
            SAD = 0,
            ANGRY = 1,
            HAPPY = 2
        }

        public emotionType emotion;
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hitNPC(target, hit, damageDone);
        }

        public virtual void hitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (emotion == emotionType.SAD)
            {
                if (!target.HasBuff<Happy>() && !target.HasBuff<Angry>())
                {
                    target.AddBuff(ModContent.BuffType<Sad>(), 600);
                }
            }
            else if (emotion == emotionType.ANGRY)
            {
                if (!target.HasBuff<Happy>() && !target.HasBuff<Sad>())
                {
                    target.AddBuff(ModContent.BuffType<Angry>(), 600);
                }
            }
            else if (emotion == emotionType.HAPPY)
            {
                if (!target.HasBuff<Angry>() && !target.HasBuff<Sad>())
                {
                    target.AddBuff(ModContent.BuffType<Happy>(), 600);
                }
            }
        }

    }

}
