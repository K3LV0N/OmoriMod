using OmoriMod.Items.BossRelated.YeOldSproutWeapons;
using OmoriMod.Items.Health;
using OmoriMod.NPCs.Bosses.YeOldSproutFile;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.BossRelated.BossBags
{
    public class YeOldBossBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            // This set is one that every boss bag should have.
            // It will create a glowing effect around the item when dropped in the world.
            // It will also let our boss bag drop dev armor..
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true; // ..But this set ensures that dev armor will only be dropped on special world seeds, since that's the behavior of pre-hardmode boss bags.

            Item.ResearchUnlockCount = 3;
        }
        public override void SetDefaults()
        {
            // consumability and stack size
            Item.consumable = true;
            Item.maxStack = 9999;
            
            // size
            Item.width = 32;
            Item.height = 32;

            // rarity
            Item.rare = ItemRarityID.Purple;

            // expert mode only
            Item.expert = true; // This makes sure that "Expert" displays in the tooltip and the item name color changes
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            int[] weaponOptions = new int[2];
            weaponOptions[0] = ModContent.ItemType<SproutShotgun>();
            weaponOptions[1] = ModContent.ItemType<SproutScythe>();
            itemLoot.Add(ItemDropRule.OneFromOptions(1, weaponOptions));
       

            itemLoot.Add(ItemDropRule.Common(ItemID.LesserHealingPotion, 1, 8, 12));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tofu>(), 1, 30, 50));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<YeOldSprout>()));

        }
    }
}
