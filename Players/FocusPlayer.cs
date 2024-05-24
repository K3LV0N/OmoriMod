using Terraria.ModLoader;

namespace OmoriMod.Players
{
    public class FocusPlayer : ModPlayer
    {
        public bool SomethingPet;
        public bool hasChargeItem;
        public bool reachedMaxCharge;
        public int currentCharge;
        public int maxCharge;

        public override void ResetEffects()
        {
            SomethingPet = false;
            hasChargeItem = false;
            currentCharge = 0;
            maxCharge = 0;
        }
    }
}
