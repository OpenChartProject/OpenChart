using System.Collections.Generic;

namespace OpenChart.Formats.Osu.Data
{
    /// <summary>
    /// osu! beatmap event information. These are used for things such as backgrounds, videos and beatmap breaks.
    /// TODO: If background support is implemented, add classes for background event information.
    /// </summary>
    public class OsuBeatmapEvent
    {
        /// <summary>
        /// The unique identifier of this event.
        /// </summary>
        public string EventType { get; set; }
        
        /// <summary>
        /// The number of milliseconds from the start of the beatmap when this event should be triggered.
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// A list of arbitrary event parameters for this event.
        /// </summary>
        public List<string> EventParams { get; set; }
    }
}