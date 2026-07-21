namespace OmoriMod.Content.Systems.EmotionSystem;

/// <summary>
/// Identifies the emotion associated with an emotion-aware entity, item, projectile, or buff.
/// </summary>
public enum EmotionType
{
    None = 0,
    Sad = 1,
    Angry = 2,
    Happy = 3,
}

/// <summary>
/// Distinguishes registrations that share an emotion and tier but differ in buff-duration behavior.
/// </summary>
public enum EmotionBuffVariant
{
    /// <summary>A timed buff that participates in normal application and tier promotion.</summary>
    Standard,
    /// <summary>An untimed-display buff intended for persistent sources such as accessories.</summary>
    NoTime,
}
