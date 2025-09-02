using System;
using Terraria.ModLoader.IO;

namespace OmoriMod.Util
{
    /// <summary>
    /// A utility class that helps with timing. Can run for a really long time.
    /// </summary>
    public class TickTimer
    {
        /// <summary>
        /// <see cref="String"/> for saving purposes.
        /// </summary>
        private static readonly String saveTotalTicks = "TickTimer:totalTicks:";

        /// <summary>
        /// <see cref="String"/> for saving purposes.
        /// </summary>
        private static readonly String saveOriginalTicks = "TickTimer:originalTicks:";

        /// <summary>
        /// How many total ticks there are left in this <see cref="TickTimer"/>
        /// </summary>
        private long _totalTicks;

        /// <summary>
        /// How many total ticks this <see cref="TickTimer"/> was initialized with
        /// </summary>
        private readonly long _originalTicks;

        /// <summary>
        /// How many hours are left in the <see cref="TickTimer"/>
        /// </summary>
        public long Hours => _totalTicks / 216000;          // 60 * 60 * 60
        /// <summary>
        /// How many minutes are left in the <see cref="TickTimer"/>
        /// </summary>
        public long Minutes => (_totalTicks / 3600) % 60;     // 60 * 60

        /// <summary>
        /// How many seconds are left in the <see cref="TickTimer"/>
        /// </summary>
        public long Seconds => (_totalTicks / 60) % 60;

        /// <summary>
        /// How many ticks are left in the <see cref="TickTimer"/>
        /// </summary>
        public long Ticks => _totalTicks % 60;

        // --- Constructors ---

        /// <summary>
        /// Creates a <see cref="TickTimer"/> with its total ticks starting at 0.
        /// </summary>
        public TickTimer()
        {
            _totalTicks = 0;
            _originalTicks = _totalTicks;
        }

        /// <summary>
        /// Copies the <paramref name="originalTimer"/> to make a new <see cref="TickTimer"/>
        /// </summary>
        /// <param name="originalTimer">The original <see cref="TickTimer"/> to copy.</param>
        public TickTimer(TickTimer originalTimer)
        {
            _totalTicks = originalTimer._totalTicks;
            _originalTicks = originalTimer._originalTicks;
        }

        /// <summary>
        /// Creates a <see cref="TickTimer"/> from ticks.
        /// </summary>
        /// <param name="totalTicks"></param>
        public TickTimer(long totalTicks)
        {
            _totalTicks = Math.Max(0, totalTicks);
            _originalTicks = _totalTicks;
        }

        /// <summary>
        /// Creates a <see cref="TickTimer"/> from these time chunks.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="ticks"></param>
        public TickTimer(long seconds, long ticks)
        {
            _totalTicks =
                (seconds * 60) +
                ticks;

            if (_totalTicks < 0) _totalTicks = 0; // clamp safety
            _originalTicks = _totalTicks;
        }

        /// <summary>
        /// Creates a <see cref="TickTimer"/> from these time chunks.
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="ticks"></param>
        public TickTimer(long minutes, long seconds, long ticks)
        {
            _totalTicks =
                (minutes * 3600) +
                (seconds * 60) +
                ticks;

            if (_totalTicks < 0) _totalTicks = 0; // clamp safety
            _originalTicks = _totalTicks;
        }

        /// <summary>
        /// Creates a <see cref="TickTimer"/> from these time chunks.
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="ticks"></param>
        public TickTimer(long hours, long minutes, long seconds, long ticks)
        {
            _totalTicks =
                (hours * 216000) +
                (minutes * 3600) +
                (seconds * 60) +
                ticks;

            if (_totalTicks < 0) _totalTicks = 0; // clamp safety
            _originalTicks = _totalTicks;
        }

        /// <summary>
        /// Creates a <see cref="TickTimer"/> from the saved <paramref name="tag"/> data. Needs an <paramref name="identifier"/> to figure out which timer it is.
        /// </summary>
        /// <param name="tag">The <see cref="TagCompound"/> that stores this <see cref="TickTimer"/> data.</param>
        /// <param name="identifier">The <see cref="String"/> name of this <see cref="TickTimer"/>.</param>
        public TickTimer(TagCompound tag, String identifier)
        {
            _totalTicks = tag.GetLong(OmoriMod.MOD_NAME + identifier + saveTotalTicks);
            _originalTicks = tag.GetLong(OmoriMod.MOD_NAME + identifier + saveOriginalTicks);
        }

        /// <summary>
        /// Creates a <see cref="TickTimer"/> from the saved <paramref name="tag"/> data. Needs an <paramref name="identifier"/> to figure out which timer it is.
        /// If no <see cref="TickTimer"/> is found, this instance is set to <paramref name="fallbackTimer"/>.
        /// </summary>
        /// <param name="tag">The <see cref="TagCompound"/> that stores this <see cref="TickTimer"/> data.</param>
        /// <param name="identifier">The <see cref="String"/> name of this <see cref="TickTimer"/>.</param>
        /// <param name="fallbackTimer">A backup <see cref="TickTimer"/> if this instance is not found in the <paramref name="tag"/>.</param>
        public TickTimer(TagCompound tag, String identifier, TickTimer fallbackTimer)
        {
            if (tag.ContainsKey(OmoriMod.MOD_NAME + identifier + saveTotalTicks))
            {
                _totalTicks = tag.GetLong(OmoriMod.MOD_NAME + identifier + saveTotalTicks);
                _originalTicks = tag.GetLong(OmoriMod.MOD_NAME + identifier + saveOriginalTicks);
            }
            else
            {
                _totalTicks = fallbackTimer._totalTicks;
                _originalTicks = fallbackTimer._originalTicks;
            }
        }



        /// <summary>
        /// Resets the <see cref="_totalTicks"/> to <see cref="_originalTicks"/>.
        /// </summary>
        public void Reset()
        {
            _totalTicks = _originalTicks;
        }


        /// <summary>
        /// Increments <see cref="_totalTicks"/> by 1.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TickTimer operator ++(TickTimer t)
        {
            t._totalTicks++;
            return t;
        }

        /// <summary>
        /// Decrements <see cref="_totalTicks"/> by 1.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TickTimer operator --(TickTimer t)
        {
            if (t._totalTicks > 0)
                t._totalTicks--;
            return t;
        }


        /// <summary>
        /// Creates a savable <see cref="TagCompound"/> out of this <see cref="TickTimer"/>.
        /// </summary>
        /// <param name="identifier">The <see cref="String"/> name of this <see cref="TickTimer"/>.</param>
        /// <returns></returns>
        public TagCompound CreateTagCompound(String identifier)
        {
            TagCompound tag = new()
            {
                [OmoriMod.MOD_NAME + identifier + saveTotalTicks] = _totalTicks,
                [OmoriMod.MOD_NAME + identifier + saveOriginalTicks] = _originalTicks
            };
            return tag;
        }

        /// <summary>
        /// Saves this <see cref="TickTimer"/> on the provided <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">The <see cref="TagCompound"/> this <see cref="TickTimer"/> should be saved to.</param>
        /// <param name="identifier">The <see cref="String"/> name of this <see cref="TickTimer"/>.</param>

        public void SaveData(TagCompound tag, String identifier)
        {
            tag[OmoriMod.MOD_NAME + identifier + saveTotalTicks] = _totalTicks;
            tag[OmoriMod.MOD_NAME + identifier + saveOriginalTicks] = _originalTicks;
        }


        /// <summary>
        /// Returns the total ticks that are left in this <see cref="TickTimer"/>
        /// </summary>
        public long TotalTicks => _totalTicks;

        /// <summary>
        /// Returns <paramref name="true"/> if this <see cref="TickTimer"/> is out of ticks. Only useful for <see cref="TickTimer"/> instances that decrement
        /// </summary>
        public bool IsDone => _totalTicks <= 0;



        public override bool Equals(object obj)
        {
            if (obj is TickTimer other)
                return _totalTicks == other._totalTicks;
            return false;
        }

        public override int GetHashCode()
        {
            return _totalTicks.GetHashCode();
        }
    }
}
