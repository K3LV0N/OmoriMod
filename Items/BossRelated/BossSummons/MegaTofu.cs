using OmoriMod.Items.Health;
using OmoriMod.NPCs.Bosses.YeOldSproutFile;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.BossRelated.BossSummons
{
    public class MegaTofu : ModItem
    {
        public override void SetDefaults()
        {
            // consumability and stack size
            Item.consumable = false;
            Item.maxStack = 1;

            // size
            Item.width = 16;
            Item.height = 16;

            // usage
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.autoReuse = false;

            // rarity
            Item.rare = ItemRarityID.Purple;

            // price
            Item.value = Item.buyPrice(platinum: 0, gold: 0, silver: 15, copper: 0);

        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<YeOldSprout>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                // If the player using the item is the client
                // (explicitely excluded serverside here)
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<YeOldSprout>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the player is not in multiplayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    // If the player is in multiplayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe r1 = CreateRecipe();
            r1.AddIngredient<Tofu>(10); 
            r1.Register();
        }
    }
}
