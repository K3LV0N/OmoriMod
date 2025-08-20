using Microsoft.Xna.Framework;
using OmoriMod.Systems.ChargeBar;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmoriMod.Systems
{
    public class OmoriModUISystem : ModSystem
    {
        private UserInterface chargeBarInterface;
        internal ChargeBarUI chargeBar;

        public override void Load()
        {
            // A server doesn't need UI lol
            if (!Main.dedServ)
            {
                chargeBar = new ChargeBarUI();
                chargeBarInterface = new UserInterface();
                chargeBarInterface.SetState(chargeBar);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            chargeBarInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            // choose which layer to put our UI. This layer works for us
            int chargeBarUIIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 2"));
            if (chargeBarUIIndex != -1)
            {
                layers.Insert(chargeBarUIIndex, new LegacyGameInterfaceLayer("ChargeBarBelow",
                    delegate
                    {
                        chargeBarInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
    }
}
