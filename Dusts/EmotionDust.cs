using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Dusts
{
    public class EmotionDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale = 1f;
        }

    }
}
