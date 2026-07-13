using Terraria.ModLoader;
using OmoriMod.Systems.AbilitySystem.ItemAbilities.Registries;

namespace OmoriMod.Systems.AbilitySystem
{
    public class AbilitySystem : ModSystem
    {
        public override void PostSetupContent()
        {
            PassiveAbilityRegistry.Initialize();
            ActiveAbilityRegistry.Initialize();
        }
    }
}
