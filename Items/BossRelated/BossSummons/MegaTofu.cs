﻿using OmoriMod.Items.Abstract_Classes;
using OmoriMod.Items.Health;
using OmoriMod.NPCs.Bosses.YeOldSproutFile;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Items.BossRelated.BossSummons
{
    public class MegaTofu : OmoriModItem
    {
        MegaTofu()
        {
            itemTypeForResearch = ItemTypeForResearch.TreasureBag_BossSummons_Dye;
        }
        public override void SetDefaults()
        {
            ItemDefaults(
                width: 16,
                height: 16,
                scale: 1,
                buyPrice: Item.buyPrice(platinum: 0, gold: 0, silver: 15, copper: 0),
                stackSize: 1,
                consumable: false
                );

            AnimationDefaults(
                useTime: 20,
                useStyleID: ItemUseStyleID.HoldUp,
                useSound: SoundID.Roar,
                autoReuse: false
                );

            SetItemRarity(ItemRarityID.Purple);
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
