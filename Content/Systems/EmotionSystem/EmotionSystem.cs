using System;
using System.Collections.Generic;

using OmoriMod.Content.Buffs.Abstract;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OmoriMod.Content.Systems.EmotionSystem;

public class EmotionSystem : ModSystem
{
    private readonly record struct EmotionLookupKey(EmotionType Emotion, int Tier, EmotionBuffVariant Variant);
    private readonly record struct EmotionFamilyLookupKey(Type FamilyType, int Tier, EmotionBuffVariant Variant);
    private readonly record struct EmotionBuffMetadata(EmotionType Emotion, int Tier, EmotionBuffVariant Variant);

    private static readonly Dictionary<EmotionLookupKey, int> EmotionBuffTypes = [];
    private static readonly Dictionary<EmotionFamilyLookupKey, int> EmotionBuffTypesByFamily = [];
    private static readonly Dictionary<int, EmotionBuffMetadata> EmotionMetadataByBuffType = [];
    private static readonly Dictionary<EmotionType, int> MaxEmotionTierByType = [];

    /// <summary>
    /// How long emotions last on entities
    /// </summary>
    public const int EMOTION_TIME_IN_SECONDS = 60;

    /// <summary>
    /// The additional source damage scaling per emotional advantage level multiplier
    /// </summary>
    public const float EMOTIONAL_ADVANTAGE_VALUE_PER_LEVEL = 0.07f;

    /// <summary>
    /// The max emotionLevel of emotions for Players (including endlessly scaling ones)
    /// </summary>
    public const int PLAYER_MAX_EMOTION_LEVEL = 43;

    /// <summary>
    /// The max emotionLevel of emotions for NPCs
    /// </summary>
    public const int NPC_MAX_EMOTION_LEVEL = 1;


    /// <summary>
    /// Ensures that each <paramref name="registry"/> does not try to contain duplicate values (or overwrite values)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="registry"></param>
    /// <param name="key"></param>
    /// <param name="emotionBuff"></param>
    /// <exception cref="InvalidOperationException"></exception>
    private static void AddUniqueRegistration<TKey>(Dictionary<TKey, int> registry, TKey key, EmotionBuff emotionBuff)
        where TKey : notnull
    {
        if (registry.TryGetValue(key, out int existingBuffType))
        {
            ModBuff existingBuff = ModContent.GetModBuff(existingBuffType);
            string existingName = existingBuff?.FullName ?? existingBuffType.ToString();
            throw new InvalidOperationException(
                $"Duplicate emotion buff registration for {key}: '{existingName}' and '{emotionBuff.FullName}'.");
        }

        registry.Add(key, emotionBuff.Type);
    }

    /// <summary>
    /// Registers a specific <see cref="EmotionBuff"/> into <see cref="EmotionBuffTypes"/>, and <see cref="EmotionBuffTypesByfamily"/>
    /// </summary>
    /// <param name="emotionBuff"></param>
    /// <param name="metadata"></param>
    private static void RegisterEmotionBuff(EmotionBuff emotionBuff, EmotionBuffMetadata metadata)
    {
        EmotionLookupKey emotionKey = new(metadata.Emotion, metadata.Tier, metadata.Variant);
        AddUniqueRegistration(EmotionBuffTypes, emotionKey, emotionBuff);

        Type familyType = emotionBuff.GetType();
        while (familyType != null
            && familyType != typeof(EmotionBuff)
            && typeof(EmotionBuff).IsAssignableFrom(familyType))
        {
            EmotionFamilyLookupKey familyKey = new(familyType, metadata.Tier, metadata.Variant);
            AddUniqueRegistration(EmotionBuffTypesByFamily, familyKey, emotionBuff);
            familyType = familyType.BaseType;
        }
    }

    /// <summary>
    /// Populates <see cref="EmotionBuffTypes"/>, <see cref="EmotionBuffTypesByFamily"/>, <see cref="EmotionMetadataByBuffType"/>, and <see cref="MaxEmotionTierByType"/>
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public override void PostSetupContent()
    {
        EmotionBuffTypes.Clear();
        EmotionBuffTypesByFamily.Clear();
        EmotionMetadataByBuffType.Clear();
        MaxEmotionTierByType.Clear();

        for (int buffType = 0; buffType < BuffLoader.BuffCount; buffType++)
        {
            if (ModContent.GetModBuff(buffType) is not EmotionBuff emotionBuff)
            {
                continue;
            }

            int tier = emotionBuff.emotionLevel;
            if (tier < 1)
            {
                throw new InvalidOperationException(
                    $"Emotion buff '{emotionBuff.FullName}' must declare an emotion level of at least 1, but declared {tier}.");
            }

            EmotionBuffVariant variant = Main.buffNoTimeDisplay[buffType]
                ? EmotionBuffVariant.NoTime
                : EmotionBuffVariant.Standard;
            EmotionBuffMetadata metadata = new(emotionBuff.Emotion, tier, variant);

            RegisterEmotionBuff(emotionBuff, metadata);
            EmotionMetadataByBuffType.Add(buffType, metadata);

            if (variant == EmotionBuffVariant.Standard
                && (!MaxEmotionTierByType.TryGetValue(emotionBuff.Emotion, out int currentMax) || tier > currentMax))
            {
                MaxEmotionTierByType[emotionBuff.Emotion] = tier;
            }
        }

        ValidateStandardEmotionTiers();
    }
    
    /// <summary>
    /// Makes sure that <see cref="MaxEmotionTierByType"/> is formatted correctly
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private static void ValidateStandardEmotionTiers()
    {
        foreach ((EmotionType emotion, int maxTier) in MaxEmotionTierByType)
        {
            if (maxTier > PLAYER_MAX_EMOTION_LEVEL)
            {
                throw new InvalidOperationException(
                    $"Emotion '{emotion}' has a final tier of {maxTier}, which exceeds PLAYER_MAX_EMOTION_LEVEL ({PLAYER_MAX_EMOTION_LEVEL}).");
            }

            for (int tier = 1; tier <= maxTier; tier++)
            {
                EmotionLookupKey key = new(emotion, tier, EmotionBuffVariant.Standard);
                if (!EmotionBuffTypes.ContainsKey(key))
                {
                    throw new InvalidOperationException(
                        $"Emotion '{emotion}' is missing standard tier {tier}. Standard emotion tiers must be contiguous from 1 through {maxTier}.");
                }
            }
        }
    }


    /// <summary>
    /// A general function that allows the generalization of Entity's
    /// </summary>
    /// <param name="entity">The owner of the bufflist returned</param>
    /// <returns>The bufflist of the entity</returns>
    private static int[] GetBuffListOfEntity(Entity entity)
    {
        return (entity is NPC npc)
            ? npc.buffType
            : (entity is Player player)
            ? player.buffType
            : [];
    }

    /// <summary>
    /// gets the typing of the <see cref="EmotionBuff"/> that matches the <paramref name="emotion"/>, <paramref name="emotionLevel"/>, and <paramref name="variant"/>
    /// </summary>
    /// <param name="emotion">The <see cref="EmotionType"/> of this <see cref="EmotionBuff"/></param>
    /// <param name="emotionLevel">The emotionLevel of this <see cref="EmotionBuff"/></param>
    /// <param name="variant">What the variant of this <see cref="EmotionBuff"/> is</param>
    /// <returns>The Type of the corresponding <see cref="EmotionBuff"/>, if there is one.</returns>
    public static int? GetEmotionBuffType(
        EmotionType emotion,
        int emotionLevel,
        EmotionBuffVariant variant = EmotionBuffVariant.Standard)
    {
        EmotionLookupKey key = new(emotion, emotionLevel, variant);
        return EmotionBuffTypes.TryGetValue(key, out int buffType) ? buffType : null;
    }

    /// <summary>
    /// gets the typing of the <see cref="EmotionBuff"/> that matches the same <see cref="EmotionBuff"/> subtype, <paramref name="emotionLevel"/>, and <paramref name="variant"/>
    /// </summary>
    /// <typeparam name="T">An <see cref="EmotionBuff"/> in the same family as the targeted one.</typeparam>
    /// <param name="emotionLevel">The emotionLevel of this <see cref="EmotionBuff"/></param>
    /// <param name="variant">What the variant of this <see cref="EmotionBuff"/> is</param>
    /// <returns>The Type of the corresponding <see cref="EmotionBuff"/>, if there is one.</returns>
    private static int? GetEmotionBuffType<T>(
        int emotionLevel,
        EmotionBuffVariant variant = EmotionBuffVariant.Standard)
        where T : EmotionBuff
    {
        EmotionFamilyLookupKey key = new(typeof(T), emotionLevel, variant);
        return EmotionBuffTypesByFamily.TryGetValue(key, out int buffType) ? buffType : null;
    }

    /// <summary>
    /// returns the type of the <see cref="EmotionBuff"/> currently on the <see cref="Entity"/>. If no buff exists, returns null.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> that is being checked</param>
    /// <returns>The buffType of the <see cref="EmotionBuff"/> currentlt on the <see cref="Entity"/>.</returns>
    public static int? GetEmotionType(Entity entity)
    {
        int[] buffs = GetBuffListOfEntity(entity);
        foreach (int buffID in buffs)
        {
            if (ModContent.GetModBuff(buffID) is EmotionBuff currentBuff)
            {
                return currentBuff.Type;
            }
        }
        return null;
    }

    /// <summary>
    /// Gets the next tier of emotion for the <paramref name="currentEmotion"/>, if there is one.
    /// </summary>
    /// <typeparam name="T">The type of the current emotion</typeparam>
    /// <param name="currentEmotion">The <see cref="EmotionBuff"/> that is being checked</param>
    /// <returns>The next tier of <see cref="EmotionBuff"/> for <paramref name="currentEmotion"/>, if there is one.</returns>
    private static int? GetNextTierEmotionType<T>(T currentEmotion) where T : EmotionBuff
    {
        if (!EmotionMetadataByBuffType.TryGetValue(currentEmotion.Type, out EmotionBuffMetadata metadata)
            || IsFinalEmotionTier(currentEmotion.Type))
        {
            return null;
        }

        return GetEmotionBuffType(metadata.Emotion, metadata.Tier + 1);
    }

    /// <summary>
    /// Returns the emotion tier of the <see cref="EmotionBuff"/> with type <paramref name="buffType"/>
    /// </summary>
    /// <param name="buffType">The Type of the <see cref="EmotionBuff"/> that is being checked.</param>
    /// <returns>The tier of the <see cref="EmotionBuff"/> if there is one.</returns>
    public static int? GetEmotionTier(int buffType)
    {
        return EmotionMetadataByBuffType.TryGetValue(buffType, out EmotionBuffMetadata metadata)
            ? metadata.Tier
            : null;
    }

    /// <summary>
    /// Returns the max tier of an <see cref="EmotionType"/>.
    /// </summary>
    /// <param name="emotion">The <see cref="EmotionType"/> of the <see cref="EmotionBuff"/> being checked</param>
    /// <returns>The max tier of the <see cref="EmotionBuff"/> if there is one.</returns>
    public static int? GetMaxEmotionTier(EmotionType emotion)
    {
        return MaxEmotionTierByType.TryGetValue(emotion, out int maxTier) ? maxTier : null;
    }

    /// <summary>
    /// Checks to see if the emotion of <paramref name="buffType"/> is the final tier of that <see cref="EmotionType"/>
    /// </summary>
    /// <param name="buffType">The buffType of the <see cref="EmotionBuff"/></param>
    /// <returns><c>True</c> if the <see cref="EmotionBuff"/> is the final tier, <c>False</c> otherwise</returns>
    public static bool IsFinalEmotionTier(int buffType)
    {
        return EmotionMetadataByBuffType.TryGetValue(buffType, out EmotionBuffMetadata metadata)
            && metadata.Variant == EmotionBuffVariant.Standard
            && MaxEmotionTierByType.TryGetValue(metadata.Emotion, out int maxTier)
            && metadata.Tier == maxTier;
    }


    public static int GetEmotionLevel(IEmotionEntity entity)
    {
        return entity.ActiveEmotionBuff != null ? entity.ActiveEmotionBuff.emotionLevel : 0;
    }

    /// <summary>
    /// Returns the emotional advantage level of the attacker and the target.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns><c>0</c> means no advantage. 
    /// Any <c>positive value</c> means the attacker has advantage. 
    /// Any <c>negative value</c> means the defender has advantage.</returns>
    public static int CalculateAdvantage(IEmotionEntity attacker, IEmotionEntity defender)
    {
        bool? attackerAdvantage = attacker.CheckForAdvantage(defender);

        // checks if attackerAdvantage is null, if it is return 0
        // if an advantage exists, return the advantage
        return attackerAdvantage == null
            ? 0
            : attackerAdvantage == true
            ? GetEmotionLevel(attacker) - GetEmotionLevel(defender) + 1
            : GetEmotionLevel(defender) - GetEmotionLevel(attacker) + 1;
    }

    /// <summary>
    /// Removes a specific <see cref="EmotionBuff"/> off of an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="emotionType"></param>
    private static void RemoveEmotion(Entity entity, int emotionType)
    {
        if (entity is NPC npc)
        {
            if (Main.dedServ || Main.netMode == NetmodeID.SinglePlayer)
            {
                npc.DelBuff(npc.FindBuffIndex(emotionType));
            }
            else
            {
                npc.RequestBuffRemoval(emotionType);
            }
        }
        if (entity is Player player)
        {
            player.ClearBuff(emotionType);
        }
    }


    /// <summary>
    /// Clears all the <see cref="EmotionBuff"/> off an entity
    /// </summary>
    /// <param name="entity">The entity scheduling the removal</param>
    public static void ClearAllEmotions(Entity entity)
    {
        int[] buffs = GetBuffListOfEntity(entity);
        foreach (int buffID in buffs)
        {
            if (ModContent.GetModBuff(buffID) is EmotionBuff)
            {
                RemoveEmotion(entity, buffID);
            }
        }
    }

    /// <summary>
    /// Removes any emotions that are incompatible with the provided emotion type T.
    /// </summary>
    public static void RemoveIncompatibleEmotions<T>(Entity entity) where T : EmotionBuff
    {
        int? representativeBuffType = GetEmotionBuffType<T>(1);
        if (!representativeBuffType.HasValue
            || ModContent.GetModBuff(representativeBuffType.Value) is not EmotionBuff buffInstance)
        {
            return;
        }

        int[] buffs = GetBuffListOfEntity(entity);

        List<int> buffsToRemove = [];
        foreach (int buffID in buffs)
        {
            ModBuff modBuff = ModContent.GetModBuff(buffID);
            if (modBuff is EmotionBuff currentBuff && buffInstance.IsIncompatibleWith(currentBuff))
            {
                buffsToRemove.Add(buffID);
            }
        }
        foreach (int id in buffsToRemove) RemoveEmotion(entity, id);
    }

    /// <summary>
    /// Checks to see if the <see cref="EmotionBuff"/> of type <typeparamref name="T"/> can be applied to the <paramref name="entity"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="EmotionBuff"/> to be applied</typeparam>
    /// <param name="entity">The <see cref="Entity"/> this check is running on</param>
    /// <returns></returns>
    public static bool CanApplyEmotion<T>(Entity entity) where T : EmotionBuff
    {
        int? representativeBuffType = GetEmotionBuffType<T>(1);
        if (!representativeBuffType.HasValue
            || ModContent.GetModBuff(representativeBuffType.Value) is not EmotionBuff buffInstance)
        {
            return false;
        }

        int[] buffs = GetBuffListOfEntity(entity);
        foreach (int buffID in buffs)
        {
            ModBuff modBuff = ModContent.GetModBuff(buffID);
            // Check if current buff is incompatible with T
            if (modBuff is EmotionBuff currentBuff && buffInstance.IsIncompatibleWith(currentBuff))
            {
                return false;
            }
        }
        return true;
    }
    

    /// <summary>
    /// checks to see an <see cref="EmotionBuff"/> of Type <paramref name="buffType"/> can be promoted.
    /// </summary>
    /// <param name="buffType">the Type of the <see cref="EmotionBuff"/> being checked</param>
    /// <returns><c>True</c> if promotable, <c>False</c> otherwise</returns>
    private static bool IsPromotableEmotion(int buffType, int? currentTier, int? maxTier)
    {
        EmotionBuffMetadata buffData = EmotionMetadataByBuffType[buffType];
        return currentTier.HasValue && maxTier.HasValue && (buffData.Variant == EmotionBuffVariant.Standard);
    }

    private static void PromoteEmotion<T>(T currentEmotion, Player player, int duration, bool canPromoteToFinalTier) where T : EmotionBuff
    {
        int? currentTier = GetEmotionTier(currentEmotion.Type);
        int? maxTier =  GetMaxEmotionTier(currentEmotion.Emotion);
        if (!IsPromotableEmotion(currentEmotion.Type, currentTier, maxTier)) {return;}

        if (currentTier.Value == maxTier.Value)
        {
            player.AddBuff(currentEmotion.Type, duration);
            return;
        }

        bool isTierBeforeFinal = currentTier.Value == maxTier.Value - 1;
        if (isTierBeforeFinal && !canPromoteToFinalTier)
        {
            player.AddBuff(currentEmotion.Type, duration);
            return;
        }

        int? nextEmotionType = GetNextTierEmotionType(currentEmotion);
        if (nextEmotionType.HasValue)
        {
            player.ClearBuff(currentEmotion.Type);
            player.AddBuff(nextEmotionType.Value, duration);
        }
    }

    

    public static void ApplyOrPromoteEmotion<T>(Player player, int duration, bool canPromoteToFinalTier = false) where T : EmotionBuff
    {
        // first, remove incompatible emotions
        RemoveIncompatibleEmotions<T>(player);

        // next, check if the player has a promotable emotion
        foreach (int buffID in player.buffType)
        {
            if (ModContent.GetModBuff(buffID) is T currentEmotion)
            {
                // promotable emotion found, now promote
                PromoteEmotion<T>(currentEmotion, player, duration, canPromoteToFinalTier);
                return;
            }
        }

        // promotable emotion not found, apply tier 1 version of emotion
        int? buffType = GetEmotionBuffType<T>(1);
        if (buffType.HasValue)
        {
            player.AddBuff(buffType.Value, duration);
        }
    }
}
