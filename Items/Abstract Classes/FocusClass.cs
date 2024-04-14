using OmoriMod.Players;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace OmoriMod.Items.Abstract_Classes
{
    public abstract class FocusClass : ModItem
    {
        // Set these to 0 on initialization
        // timers keeps track of ticks for charge / decay
        public int charge;
        public int timer;
        public int chargeTimer;
        public int decayTimer;

        // set max charge as x * 60
        public int maxCharge;

        // dps will increase the damage by dps every second
        public int dps;
        
        // These will be the downtimes before decay and charging start
        public int timeUntilChargeStarts;
        public int timeUntilDecayStarts;

        // how fast this weapon should decay
        public int decayRate;

        // Set to false on initialization
        public bool charging;

        // Set to true on initialization
        public bool decaying;

        public virtual void HoldItemFocus(Player player)
        {
            player.GetModPlayer<OmoriPlayer>().hasChargeItem = true;
            player.GetModPlayer<OmoriPlayer>().currentCharge = charge;
            player.GetModPlayer<OmoriPlayer>().maxCharge = maxCharge;
            if (!Main.mouseLeft)
            {
                decaying = false;
                if (charging)
                {
                    if (charge < maxCharge)
                    {
                        charge++;
                    }
                    timer = 0;
                }
                else if (chargeTimer == timeUntilChargeStarts)
                {
                    chargeTimer = 0;
                    charging = true;
                }
                else
                {
                    chargeTimer++;
                }
            }

            if (Main.mouseLeft)
            {
                charging = false;
            }
        }

        public virtual void UpdateInventoryFocus(Player player)
        {
            if (decaying)
            {
                // Decay at the rate of 1 charge per "decayTimer" ticks
                decayTimer++;
                if (charge > 0 && decayTimer == decayRate)
                {
                    charge--;
                    decayTimer = 0;
                }

            }
            else if (timer == timeUntilDecayStarts)
            {
                decaying = true;
                decayTimer = 0;
                charging = false;
                timer = 0;
            }
            else
            {
                timer++;
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            // Int variables that change over time
            writer.Write(charge);
            writer.Write(timer);
            writer.Write(chargeTimer);
            writer.Write(decayTimer);

            // Bool variables that change over time
            writer.Write(charging);
            writer.Write(decaying);
        }

        public override void NetReceive(BinaryReader reader)
        {
            // Int variables that change over time
            charge = reader.ReadInt32();
            timer = reader.ReadInt32();
            chargeTimer = reader.ReadInt32();
            decayTimer = reader.ReadInt32();

            // Bool variables that change over time
            charging = reader.ReadBoolean();
            decaying = reader.ReadBoolean();
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.Base = Item.damage + ((charge * dps) / 60);
        }
    }
}
