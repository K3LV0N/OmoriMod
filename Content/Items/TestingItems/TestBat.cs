using Microsoft.Xna.Framework;
using OmoriMod.Content.Players;
using OmoriMod.Content.Projectiles.Friendly.Melee.Bat;
using OmoriMod.Content.Items.Abstract_Classes;
using OmoriMod.Content.Items.Abstract_Classes.BaseClasses;
using OmoriMod.Content.Items.Abstract_Classes.CompositionClasses;
using OmoriMod.Content.Items.Abstract_Classes.Interfaces;
using OmoriMod.Systems.AbilitySystem.ItemAbilities.Passives;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Content.Items.TestingItems
{
    public class TestBat : AngryItem, IAbilityItem
    {
        private ItemAbilityContainer _abilityContainer;

        private bool switchProj;
        public ItemAbilityContainer AbilityContainer { get => _abilityContainer; set => _abilityContainer = value; }

        TestBat()
        {
            itemTypeForResearch = ItemTypeForResearch.Weapons_Tools_Armor_Accessory;
            _abilityContainer = new ItemAbilityContainer();
        }
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 32,
                height: 32,
                scale: 1.5f,
                buyPrice: Item.buyPrice(0, 2, 0, 0),
                stackSize: 1,
                consumable: false
                );

            DamageDefaults(
                damageType: DamageClass.Melee,
                damage: 20,
                knockback: 6f,
                crit: 4,
                noMelee: false
                );

            ProjectileDefaults(
                ammoID: AmmoID.None,
                shootSpeed: 8f
                );

            AnimationDefaults(
                useTime: 17,
                useStyleID: ItemUseStyleID.Swing,
                useSound: SoundID.Item1,
                autoReuse: true
                );

            _abilityContainer.passiveAbility = new ShootProjectilePassiveAbility(ModContent.ProjectileType<BatProjectile>());
            switchProj = false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                player.GetModPlayer<AbilityPlayer>().abilityMenuActive = true;
                switchProj = true;
                return true;
            }
            return null;
        }

        public override void UpdateInventory(Player player)
        {
            if(!player.GetModPlayer<AbilityPlayer>().abilityMenuActive && switchProj)
            {
                _abilityContainer.passiveAbility = new ShootProjectilePassiveAbility(player.GetModPlayer<AbilityPlayer>().projectileID);
                switchProj = false;
            }
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return _abilityContainer.passiveAbility.PerformAbility(player, position, velocity, type, damage, knockback, Item, meleeWeaponProjectileMoveTime);
        }
    }
}
