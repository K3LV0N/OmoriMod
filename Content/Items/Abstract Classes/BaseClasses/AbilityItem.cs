using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using OmoriMod.Systems.AbilitySystem;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using OmoriMod.Systems.AbilitySystem.ItemAbilities.Registries;

namespace OmoriMod.Content.Items.Abstract_Classes.BaseClasses
{
    public abstract class AbilityItem : OmoriModItem
    {
        public PassiveAbilityRegistry.PassiveAbilityID CurrentPassiveAbilityID = PassiveAbilityRegistry.PassiveAbilityID.NONE;
        public PassiveAbilityRegistry.PassiveAbilityID InnatePassiveAbilityID = PassiveAbilityRegistry.PassiveAbilityID.NONE;
        
        public ActiveAbilityRegistry.ActiveAbilityID CurrentActiveAbilityID = ActiveAbilityRegistry.ActiveAbilityID.NONE;
        public ActiveAbilityRegistry.ActiveAbilityID InnateActiveAbilityID = ActiveAbilityRegistry.ActiveAbilityID.NONE;

        public override void SaveData(TagCompound tag)
        {
            base.SaveData(tag);
            tag["CurrentPassiveAbilityID"] = (int)CurrentPassiveAbilityID;
            tag["CurrentActiveAbilityID"] = (int)CurrentActiveAbilityID;
        }

        public override void LoadData(TagCompound tag)
        {
            base.LoadData(tag);
            if (tag.ContainsKey("CurrentPassiveAbilityID"))
            {
                CurrentPassiveAbilityID = (PassiveAbilityRegistry.PassiveAbilityID)tag.GetInt("CurrentPassiveAbilityID");
            }
            if (tag.ContainsKey("CurrentActiveAbilityID"))
            {
                CurrentActiveAbilityID = (ActiveAbilityRegistry.ActiveAbilityID)tag.GetInt("CurrentActiveAbilityID");
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
            writer.Write((int)CurrentPassiveAbilityID);
            writer.Write((int)CurrentActiveAbilityID);
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
            CurrentPassiveAbilityID = (PassiveAbilityRegistry.PassiveAbilityID)reader.ReadInt32();
            CurrentActiveAbilityID = (ActiveAbilityRegistry.ActiveAbilityID)reader.ReadInt32();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (CanUsePassiveAbility())
            {
                var ability = PassiveAbilityRegistry.GetAbility(EffectivePassiveAbilityID);
                if (ability != null)
                {
                    // Create Shoot Context
                    AbilityContext context = new PassiveShootAbilityContext(player, Item, source, position, velocity, type, damage, knockback);
                    bool result = ability.PerformAbility(context);
                    // prevent vanilla code from running if ability runs
                    if (result) return false;
                }
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);

            if (CanUsePassiveAbility())
            {
                var ability = PassiveAbilityRegistry.GetAbility(EffectivePassiveAbilityID);
                if (ability != null)
                {
                    AbilityContext context = new PassiveHitAbilityContext(player, Item, target, damageDone, hit.Knockback, hit.Crit);
                    ability.PerformAbility(context);
                }
            }
        }
        
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (CanUseActiveAbility())
                {
                    var ability = ActiveAbilityRegistry.GetAbility(EffectiveActiveAbilityID);
                    if (ability != null)
                    {
                        AbilityContext context = new ActiveAbilityContext(player, Item);
                        bool result = ability.PerformAbility(context);
                        if (result) return true;
                    }
                }
            }
            return base.UseItem(player);
        }
        // Use this property to get the actual ability to perform. 
        public PassiveAbilityRegistry.PassiveAbilityID EffectivePassiveAbilityID => CurrentPassiveAbilityID != PassiveAbilityRegistry.PassiveAbilityID.NONE ? CurrentPassiveAbilityID : InnatePassiveAbilityID;
        public ActiveAbilityRegistry.ActiveAbilityID EffectiveActiveAbilityID => CurrentActiveAbilityID != ActiveAbilityRegistry.ActiveAbilityID.NONE ? CurrentActiveAbilityID : InnateActiveAbilityID;
        
        public virtual bool CanUsePassiveAbility()
        {
             return EffectivePassiveAbilityID != PassiveAbilityRegistry.PassiveAbilityID.NONE;
        }

        public virtual bool CanUseActiveAbility()
        {
             return EffectiveActiveAbilityID != ActiveAbilityRegistry.ActiveAbilityID.NONE;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            if (EffectivePassiveAbilityID != PassiveAbilityRegistry.PassiveAbilityID.NONE)
            {
                // Optionally add tooltip about current ability
                // tooltips.Add(new TooltipLine(Mod, "Ability", $"Ability: {EffectivePassiveAbilityID}"));
            }
        }
    }
}
