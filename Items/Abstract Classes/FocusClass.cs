using Microsoft.Xna.Framework;
using OmoriMod.Dusts;
using OmoriMod.Players;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace OmoriMod.Items.Abstract_Classes
{
    public abstract class FocusClass : EmotionalItem
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
            // set FocusPlayer variables to the current held item
            player.GetModPlayer<FocusPlayer>().hasChargeItem = true;
            player.GetModPlayer<FocusPlayer>().currentCharge = charge;
            player.GetModPlayer<FocusPlayer>().maxCharge = maxCharge;

            bool reachedMaxCharge = player.GetModPlayer<FocusPlayer>().reachedMaxCharge;

            if (charge == maxCharge && !reachedMaxCharge)
            {
                int amtOfDust = 40;
                int positionOffSet = 6;
                int speedChange = 2;
                int scaleChange = 2;
                dustHandler(player, amtOfDust, positionOffSet, speedChange, scaleChange);
                player.GetModPlayer<FocusPlayer>().reachedMaxCharge = true;
            }
            else if (charge != maxCharge)
            {
                player.GetModPlayer<FocusPlayer>().reachedMaxCharge = false;
            }

            if (!Main.mouseLeft)
            {
                decaying = false;
                if (charging)
                {
                    if (charge < maxCharge)
                    {
                        charge++;
                        if (charge > maxCharge) {charge = maxCharge;}
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

        private void dustHandler(Player player, int amtOfDust, int pOff, int SpOff, int ScOff)
        {
            Random rand = new Random();

            int SpOffUse = SpOff * 2;

            for (int i = 0; i < amtOfDust; i++)
            {
                float xSpeed = SpOffUse * (rand.NextSingle() - 0.5f);
                float ySpeed = SpOffUse * (rand.NextSingle() - 0.5f);
                float scale = ScOff * rand.NextSingle();

                int xOffset = rand.Next(-pOff, pOff);
                int yOffset = rand.Next(-pOff, pOff);
                Vector2 position = new Vector2(player.Center.X + xOffset, player.Center.Y + yOffset);

                Dust.NewDust(position, 2, 2, ModContent.DustType<EmotionDust>(), xSpeed, ySpeed, 0, Color.LightGoldenrodYellow, scale);
            }
        }
    }
}
