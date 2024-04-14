using OmoriMod.Items.Starter;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Players
{
    public class OmoriPlayer : ModPlayer
    {
        public bool SomethingPet;
        public bool hasChargeItem;
        public int currentCharge;
        public int maxCharge;

        public override void ResetEffects()
        {
            SomethingPet = false;
            hasChargeItem = false;
            currentCharge = 0;
            maxCharge = 0;
            base.ResetEffects();
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new[]
            {
                new Item(ModContent.ItemType<Note>())
            };
        }

        /*
        public override void PostUpdateRunSpeeds()
        {
            base.PostUpdateRunSpeeds();
        }
        */
    }
}
