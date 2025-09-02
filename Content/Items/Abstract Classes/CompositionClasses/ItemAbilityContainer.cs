using OmoriMod.Systems.AbilitySystem.ItemAbilities;

namespace OmoriMod.Items.Abstract_Classes.CompositionClasses
{
    public class ItemAbilityContainer
    {

        public ItemAbilityContainer() { 
            this.activeAbility = null;
            this.passiveAbility = null;
        }

        public ItemAbilityContainer(IActiveAbility activeAbility)
        {
            this.activeAbility = activeAbility;
            this.passiveAbility = null;
        }

        public ItemAbilityContainer(IPassiveAbility passiveAbility)
        {
            this.activeAbility = null;
            this.passiveAbility = passiveAbility;
        }

        public ItemAbilityContainer(IActiveAbility activeAbility, IPassiveAbility passiveAbility)
        {
            this.activeAbility = activeAbility;
            this.passiveAbility = passiveAbility;
        }

        public ItemAbilityContainer(IPassiveAbility passiveAbility, IActiveAbility activeAbility)
        {
            this.activeAbility = activeAbility;
            this.passiveAbility = passiveAbility;
        }

        public IActiveAbility activeAbility;
        public IPassiveAbility passiveAbility;
    }
}
