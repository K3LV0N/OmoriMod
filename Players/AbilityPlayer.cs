using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace OmoriMod.Players
{

    /// <summary>
    /// Use ability IDs. Default means the defaults to the original use ability of an item
    /// It is important to note that not all use abilities are here. Some are item specific. These will be accessed with DEFAULT
    /// </summary>
    public enum UseAbilityID
    {
        DEFAULT,
        SAD_POEM,
        ANNOY,
        CHEER,
    }

    /// <summary>
    /// Passive ability IDs. Default means the passive defaults to the original passive of an item.
    /// It is important to note that not all passives are here. Some are item specific. These will be accessed with DEFAULT
    /// </summary>
    public enum PassiveAbilityID { 
        DEFAULT,
    }

    /// <summary>
    /// Keeps track of abilities the player is currently using / has equipped
    /// </summary>
    public class AbilityPlayer : ModPlayer
    {
        private UseAbilityID useAbilityID;
        private PassiveAbilityID passiveAbilityID;

        AbilityPlayer()
        {
            useAbilityID = UseAbilityID.DEFAULT;
            passiveAbilityID = PassiveAbilityID.DEFAULT;
        }


        public void SetUseAbility(UseAbilityID useAbility) { useAbilityID = useAbility; }
        public void SetPassiveAbility(PassiveAbilityID passiveAbility) { passiveAbilityID = passiveAbility; }


        public UseAbilityID GetUseAbilityID() { return useAbilityID; }
        public PassiveAbilityID GetPassiveAbilityID() { return passiveAbilityID; }

        public bool IsPassiveAShootingAbility() {
            // TODO: Add more passives. if passives are shooting then:
            // return passiveAbilityID == PassiveAbilityID.SHOOT_SOMETHING || passiveAbilityID == PassiveAbilityID.SHOOT_SOMETHING_ELSE
            return true;
        }
    }
}
