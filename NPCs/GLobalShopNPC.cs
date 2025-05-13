using Microsoft.Xna.Framework;
using OmoriMod.Buffs.AngryBuff;
using OmoriMod.Buffs.HappyBuff;
using OmoriMod.Buffs.SadBuff;
using OmoriMod.Items.Accessories;
using OmoriMod.Items.BuffItems;
using OmoriMod.Summons.Pets.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.NPCs
{
    public class GLobalShopNPC : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add<PartyPopper>();
                shop.Add<RainCloud>();
                shop.Add<AirHorn>();
                shop.Add<Something>();
            }

            if (shop.NpcType == NPCID.Dryad)
            {
                shop.Add<Flower>();
                shop.Add<DeadFlower>();
                shop.Add<BloodyFlower>();
            }
        }

        // TODO: Change modifiers to work better
        public enum DamageModifier
        {
            NOTHING = 0,
            UPLOW = 1,
            UPMEDIUM = 2,
            UPHIGH = 3,
            DOWNLOW = 4,
            DOWNMEDIUM = 5,
            DOWNHIGH = 6,
        }


        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            // NEW HITMODIFIERS STUFFS
            // new(flat add to thing, multiply by, increase to final value of stat, offset to base value of stat)

            float lightDamageChange = 0.10f;
            float medDamageChange   = 0.15f;
            float highDamageChange  = 0.25f;

            StatModifier noDMG      = modifiers.SourceDamage - .99f;

            float angryIncrease     = 0.10f;
            float enragedIncrease   = 0.15f;
            float furiousIncrease   = 0.20f;

            // implement emotional advantage
            if (player.HasBuff<Sad>())
            {
                if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage + lightDamageChange; }
                else if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage - lightDamageChange; }
                
            }
            else if (player.HasBuff<Depressed>())
            {
                if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage + medDamageChange; }
                else if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage - medDamageChange; }
            }
            else if (player.HasBuff<Miserable>())
            {
                if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage + highDamageChange; }
                else if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage - highDamageChange; }
            }
            else if (player.HasBuff<Happy>())
            {
                // 1 in 12 chance of missing!
                modifiers.SourceDamage = Main.rand.NextBool(12) ? noDMG : modifiers.SourceDamage;

                // higher chance to crit
                bool critOrNot = Main.rand.NextBool(7) ? true : false;
                if (critOrNot) { modifiers.SetCrit(); }
                

                if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage + lightDamageChange; }
                else if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage - lightDamageChange; }
            }
            else if (player.HasBuff<Ecstatic>())
            {
                // 1 in 9 chance of missing!
                modifiers.SourceDamage = Main.rand.NextBool(9) ? noDMG : modifiers.SourceDamage;

                // higher chance to crit
                bool critOrNot = Main.rand.NextBool(5) ? true : false;
                if (critOrNot) { modifiers.SetCrit(); }

                if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage + medDamageChange; }
                else if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage - medDamageChange; }
            }
            else if (player.HasBuff<Manic>())
            {
                // 1 in 6 chance of missing!
                modifiers.SourceDamage = Main.rand.NextBool(6) ? noDMG : modifiers.SourceDamage;

                // higher chance to crit
                bool critOrNot = Main.rand.NextBool(3);
                if (critOrNot) { modifiers.SetCrit(); }

                if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage + highDamageChange; }
                else if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage - highDamageChange; }
            }
            else if (player.HasBuff<Angry>())
            {
                modifiers.SourceDamage = modifiers.SourceDamage + angryIncrease;
                if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage + lightDamageChange; }
                else if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage - lightDamageChange; }
            }
            else if (player.HasBuff<Enraged>())
            {
                modifiers.SourceDamage = modifiers.SourceDamage + enragedIncrease;
                if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage + medDamageChange; }
                else if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage - medDamageChange; }
            }
            else if (player.HasBuff<Furious>())
            {
                modifiers.SourceDamage = modifiers.SourceDamage + furiousIncrease;
                if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage + highDamageChange; }
                else if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage - highDamageChange; }
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = new Player();
            if (projectile.owner == Main.myPlayer)
            {
                player = Main.player[Main.myPlayer];
            }

            // NEW HITMODIFIERS STUFFS
            // new(flat add to thing, multiply by, increase to final value of stat, offset to base value of stat)

            float lightDamageChange = 0.10f;
            float medDamageChange = 0.15f;
            float highDamageChange = 0.25f;

            StatModifier noDMG = modifiers.SourceDamage - .99f;

            float angryIncrease = 0.10f;
            float enragedIncrease = 0.15f;
            float furiousIncrease = 0.20f;

            // implement emotional advantage
            if (player.HasBuff<Sad>())
            {
                if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage + lightDamageChange; }
                else if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage - lightDamageChange; }

            }
            else if (player.HasBuff<Depressed>())
            {
                if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage + medDamageChange; }
                else if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage - medDamageChange; }
            }
            else if (player.HasBuff<Miserable>())
            {
                if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage + highDamageChange; }
                else if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage - highDamageChange; }
            }
            else if (player.HasBuff<Happy>())
            {
                // 1 in 12 chance of missing!
                modifiers.SourceDamage = Main.rand.NextBool(12) ? noDMG : modifiers.SourceDamage;

                // higher chance to crit
                bool critOrNot = Main.rand.NextBool(7) ? true : false;
                if (critOrNot) { modifiers.SetCrit(); }


                if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage + lightDamageChange; }
                else if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage - lightDamageChange; }
            }
            else if (player.HasBuff<Ecstatic>())
            {
                // 1 in 9 chance of missing!
                modifiers.SourceDamage = Main.rand.NextBool(9) ? noDMG : modifiers.SourceDamage;

                // higher chance to crit
                bool critOrNot = Main.rand.NextBool(5) ? true : false;
                if (critOrNot) { modifiers.SetCrit(); }

                if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage + medDamageChange; }
                else if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage - medDamageChange; }
            }
            else if (player.HasBuff<Manic>())
            {
                // 1 in 9 chance of missing!
                modifiers.SourceDamage = Main.rand.NextBool(6) ? noDMG : modifiers.SourceDamage;

                // higher chance to crit
                bool critOrNot = Main.rand.NextBool(3) ? true : false;
                if (critOrNot) { modifiers.SetCrit(); }

                if (npc.HasBuff<Angry>()) { modifiers.SourceDamage = modifiers.SourceDamage + highDamageChange; }
                else if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage - highDamageChange; }
            }
            else if (player.HasBuff<Angry>())
            {
                modifiers.SourceDamage = modifiers.SourceDamage + angryIncrease;
                if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage + lightDamageChange; }
                else if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage - lightDamageChange; }
            }
            else if (player.HasBuff<Enraged>())
            {
                modifiers.SourceDamage = modifiers.SourceDamage + enragedIncrease;
                if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage + medDamageChange; }
                else if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage - medDamageChange; }
            }
            else if (player.HasBuff<Furious>())
            {
                modifiers.SourceDamage = modifiers.SourceDamage + furiousIncrease;
                if (npc.HasBuff<Sad>()) { modifiers.SourceDamage = modifiers.SourceDamage + highDamageChange; }
                else if (npc.HasBuff<Happy>()) { modifiers.SourceDamage = modifiers.SourceDamage - highDamageChange; }
            }

        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            float lightDamageChange = 0.10f;
            float medDamageChange = 0.15f;
            float highDamageChange = 0.25f;

            int damage = hurtInfo.SourceDamage;

            int crit     = (int)(damage * 1.30f);
            int noDmg    = 1;
            int angryDmg = (int)(damage * 1.25f);

            float lightManaDmg = damage * .15f; // 15.0f; // 25%
            float medManaDmg = damage * .25f; // 25.0f; // 35%
            float highManaDmg = damage * .40f; // 40.0f; // 50%

            if (npc.HasBuff<Sad>())
            {
                if (target.HasBuff<Happy>()) { hurtInfo.SourceDamage = (int)(damage * (1 + lightDamageChange)); }
                else if (target.HasBuff<Ecstatic>()) { hurtInfo.SourceDamage = (int)(damage * (1 + medDamageChange)); }
                else if (target.HasBuff<Manic>()) { hurtInfo.SourceDamage = (int)(damage * (1 + highDamageChange)); }

                else if (target.HasBuff<Angry>()) { hurtInfo.SourceDamage = (int)(damage * (1 - lightDamageChange)); }
                else if (target.HasBuff<Enraged>()) { hurtInfo.SourceDamage = (int)(damage * (1 - medDamageChange)); }
                else if (target.HasBuff<Furious>()) { hurtInfo.SourceDamage = (int)(damage * (1 - highDamageChange)); }

                else if (target.HasBuff<Sad>() || target.HasBuff<Depressed>() || target.HasBuff<Miserable>())
                {
                    float manaChange;
                    float damageChange;
                    if (target.HasBuff<Sad>())
                    {
                        manaChange = lightManaDmg;
                        damageChange = lightDamageChange;
                    }
                    else if (target.HasBuff<Depressed>())
                    {
                        manaChange = medManaDmg;
                        damageChange = medDamageChange;
                    }
                    else
                    {
                        manaChange = highManaDmg;
                        damageChange = highDamageChange;
                    }



                    if ((int)(target.statMana - manaChange) > 0)
                    {
                        target.statMana = (int)(target.statMana - manaChange);
                    }
                    else
                    {
                        target.statMana = 0;
                    }
                    hurtInfo.SourceDamage = (int)(damage * (1 - damageChange));

                }

            }
            else if (npc.HasBuff<Happy>())
            {
                // 1 in 8 chance of missing!
                // change damage and not source to make sure only 1 is done
                bool missOrNot = Main.rand.NextBool(8);
                if (missOrNot) { hurtInfo.Damage = noDmg; hurtInfo.Knockback = 0; }
                else
                {
                    // 1 in 5 chance to crit
                    bool critOrNot = Main.rand.NextBool(5);
                    if (critOrNot) { hurtInfo.SourceDamage = crit; }
                }

                if (target.HasBuff<Angry>()) { hurtInfo.SourceDamage = (int)(damage * (1 + lightDamageChange)); }
                else if (target.HasBuff<Enraged>()) { hurtInfo.SourceDamage = (int)(damage * (1 + medDamageChange)); }
                else if (target.HasBuff<Furious>()) { hurtInfo.SourceDamage = (int)(damage * (1 + highDamageChange)); }
                else if (target.HasBuff<Sad>() || target.HasBuff<Depressed>() || target.HasBuff<Miserable>())
                {
                    float manaChange;
                    float damageChange;
                    if (target.HasBuff<Sad>())
                    {
                        manaChange = lightManaDmg;
                        damageChange = lightDamageChange;
                    }
                    else if (target.HasBuff<Depressed>())
                    {
                        manaChange = medManaDmg;
                        damageChange = medDamageChange;
                    }
                    else
                    {
                        manaChange = highManaDmg;
                        damageChange = highDamageChange;
                    }
                    
                    if ((int)(target.statMana - manaChange) > 0)
                    {
                        target.statMana = (int)(target.statMana - manaChange);
                    }
                    else
                    {
                        target.statMana = 0;
                    }
                    // apply the damage change 2 times
                    hurtInfo.SourceDamage = (int)(damage * (1 - damageChange));
                    hurtInfo.SourceDamage = (int)(damage * (1 - damageChange));

                }
            }
            else if (npc.HasBuff<Angry>())
            {
                hurtInfo.SourceDamage = angryDmg;

                if (target.HasBuff<Sad>() || target.HasBuff<Depressed>() || target.HasBuff<Miserable>())
                {
                    float manaChange;
                    float damageChange;
                    if (!target.HasBuff<Sad>())
                    {
                        manaChange = lightManaDmg;
                        damageChange = lightDamageChange;
                    }
                    else if (target.HasBuff<Depressed>())
                    {
                        manaChange = medManaDmg;
                        damageChange = medDamageChange;
                    }
                    else
                    {
                        manaChange = highManaDmg;
                        damageChange = highDamageChange;
                    }

                    
                    if ((int)(target.statMana - manaChange) > 0)
                    {
                        target.statMana = (int)(target.statMana - manaChange);
                    }
                    else
                    {
                        target.statMana = 0;
                    }
                    // add damage for advantage, then subract for armor bonus
                    hurtInfo.SourceDamage = (int)(damage * (1 + damageChange));
                    hurtInfo.SourceDamage = (int)(damage * (1 - damageChange));

                }

                else if (target.HasBuff<Happy>()) { hurtInfo.SourceDamage = (int)(damage * (1 - lightDamageChange)); }
                else if (target.HasBuff<Ecstatic>()) { hurtInfo.SourceDamage = (int)(damage * (1 - medDamageChange)); }
                else if (target.HasBuff<Manic>()) { hurtInfo.SourceDamage = (int)(damage * (1 - highDamageChange)); }

            }
            else if (target.HasBuff<Sad>() || target.HasBuff<Depressed>() || target.HasBuff<Miserable>())
            {
                float manaChange;
                float damageChange;
                if (target.HasBuff<Sad>())
                {
                    manaChange = lightManaDmg;
                    damageChange = lightDamageChange;
                }
                else if (target.HasBuff<Depressed>())
                {
                    manaChange = medManaDmg;
                    damageChange = medDamageChange;
                }
                else
                {
                    manaChange = highManaDmg;
                    damageChange = highDamageChange;
                }



                if ((int)(target.statMana - manaChange) > 0)
                {
                    target.statMana = (int)(target.statMana - manaChange);
                }
                else
                {
                    target.statMana = 0;
                }
                hurtInfo.SourceDamage = (int)(damage * (1 - damageChange));
            }
        }
    }
}
