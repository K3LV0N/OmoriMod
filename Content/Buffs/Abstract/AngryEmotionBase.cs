using System;

using Microsoft.Xna.Framework;

using OmoriMod.Content.NPCs.Global;
using OmoriMod.Content.Players;
using OmoriMod.Content.Systems.EmotionSystem;

using Terraria;

namespace OmoriMod.Content.Buffs.Abstract;

public abstract class AngryEmotionBase : EmotionBuff
{


    // Player Configuration

    // ===== Damage Increase =====
    private const float PLAYER_DAMAGE_INCREASE_MAX = 60.0f;
    private const float PLAYER_DAMAGE_INCREASE_RATE = 7.0f;
    private const float PLAYER_DAMAGE_INCREASE_STARTING_VALUE = 2.0f;

    // ===== Defense Decrease =====
    private const float PLAYER_DEFENSE_DECREASE_MAX = 99.9f;
    private const float PLAYER_DEFENSE_DECREASE_RATE = 12.5f;
    private const float PLAYER_DEFENSE_DECREASE_STARTING_VALUE = 12.5f;




    // NPC Configuration

    // ===== Damage Increase =====
    private const float NPC_DAMAGE_INCREASE_MAX = 50.0f;
    private const float NPC_DAMAGE_INCREASE_RATE = 5.0f;
    private const float NPC_DAMAGE_INCREASE_STARTING_VALUE = 7.0f;

    // ===== Defense Decrease =====
    private const float NPC_DEFENSE_DECREASE_MAX = 40.0f;
    private const float NPC_DEFENSE_DECREASE_RATE = 8.5f;
    private const float NPC_DEFENSE_DECREASE_STARTING_VALUE = 3.5f;




    public float GetPlayerDamageIncreasePercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: PLAYER_DAMAGE_INCREASE_MAX,
        rate: PLAYER_DAMAGE_INCREASE_RATE,
        maxEmotionLevel: EmotionSystem.PLAYER_MAX_EMOTION_LEVEL,
        startingValue: PLAYER_DAMAGE_INCREASE_STARTING_VALUE
        );

    public float GetNPCDamageIncreasePercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: NPC_DAMAGE_INCREASE_MAX,
        rate: NPC_DAMAGE_INCREASE_RATE,
        maxEmotionLevel: EmotionSystem.NPC_MAX_EMOTION_LEVEL,
        startingValue: NPC_DAMAGE_INCREASE_STARTING_VALUE
    );

    public float GetPlayerDefenseDecreasePercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: PLAYER_DEFENSE_DECREASE_MAX,
        rate: PLAYER_DEFENSE_DECREASE_RATE,
        maxEmotionLevel: EmotionSystem.PLAYER_MAX_EMOTION_LEVEL,
        startingValue: PLAYER_DEFENSE_DECREASE_STARTING_VALUE
    );

    public float GetNPCDefenseDecreasePercent(int emotionLevel) => LinearPerLevel(
        emotionLevel,
        max: NPC_DEFENSE_DECREASE_MAX,
        rate: NPC_DEFENSE_DECREASE_RATE,
        maxEmotionLevel: EmotionSystem.NPC_MAX_EMOTION_LEVEL,
        startingValue: NPC_DEFENSE_DECREASE_STARTING_VALUE
    );


    public AngryEmotionBase()
    {
        Emotion = EmotionType.ANGRY;
        dustColor = Color.Red;
    }

    public override void UpdateEmotionBuff(Player player, ref int buffIndex)
    {
        EmotionSystem.RemoveIncompatibleEmotions<AngryEmotionBase>(player);
        ModifyPlayerDefense(player, player.GetModPlayer<EmotionPlayer>().EmotionLevel);
    }

    public override void UpdateEmotionBuff(NPC npc, ref int buffIndex)
    {
        EmotionSystem.RemoveIncompatibleEmotions<AngryEmotionBase>(npc);
        ModifyNPCDefense(npc, npc.GetGlobalNPC<EmotionNPC>().EmotionLevel);
    }

    public override void ModifyPlayerOutgoingDamage(int emotionLevel, ref NPC.HitModifiers modifiers)
    {
        modifiers.SourceDamage *= 1 + GetPlayerDamageIncreasePercent(emotionLevel);
    }

    public override void ModifyNPCOutgoingDamage(int emotionLevel, ref Player.HurtModifiers modifiers)
    {
        modifiers.SourceDamage *= 1 + GetNPCDamageIncreasePercent(emotionLevel);
    }

    public override void ModifyPlayerDefense(Player player, int emotionLevel)
    {
        player.statDefense -= (int)(player.statDefense * GetPlayerDefenseDecreasePercent(emotionLevel));
    }

    public override void ModifyNPCDefense(NPC npc, int emotionLevel)
    {
        int decreasedDefense = npc.defDefense * (int)(1 - GetNPCDefenseDecreasePercent(emotionLevel));
        npc.defense = decreasedDefense;
    }

    public virtual void AngryModifyBuffText(ref string buffName, ref string tip, ref int rare) { }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        int emotionLevel = GetTooltipEmotionLevel();
        int damageUp = (int)MathF.Round(GetPlayerDamageIncreasePercent(emotionLevel) * 100);
        int defenseDown = (int)MathF.Round(GetPlayerDefenseDecreasePercent(emotionLevel) * 100);
        string buffTip = $"Attack up by {damageUp}%!" +
            $" Defense down by {defenseDown}%!";
        tip = buffTip;
        AngryModifyBuffText(ref buffName, ref tip, ref rare);
        FinalTierModifyBuffText(emotionLevel, ref buffName, ref tip, ref rare);
    }
}
