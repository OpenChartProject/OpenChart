using OpenChart.Formats.Osu.Data;

namespace OpenChart.Formats.Osu.Enum
{
    /// <summary>
    /// Effects for a given <see cref="OsuTimingPoint"/>. This is saved as a single integer, and used as a bitmask by the client.
    /// </summary>
    public class OsuTimingPointEffects
    {
        /// <summary>
        /// Bit 0: Whether kiai time is enabled. This enables some visual effects, and also affects scoring in osu!taiko mode.
        /// More information: https://osu.ppy.sh/help/wiki/Beatmap_Editor/Kiai_Time
        /// </summary>
        public bool KiaiTime { get; set; }
        
        /// <summary>
        /// Bit 3: Whether to omit the first barline in osu!taiko and osu!mania modes. This bit is ignored in other game modes.
        /// </summary>
        public bool OmitFirstBarLine { get; set; }
    }
}