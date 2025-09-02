﻿using Terraria;

namespace OmoriMod.Systems.AbilitySystem.ItemAbilities
{
    public interface IItemAbility
    { 
        public bool IsUnlocked(Item item, Player player);
        public bool IsEquippable(Item item, Player player);
        public bool PerformAbility(params object[] args);
    }
}