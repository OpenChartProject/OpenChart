using System.Collections.Generic;

namespace OpenChart.Formats.Osu.Data
{
    /// <summary>
    /// Information for the editor to use when opening a beatmap.
    /// </summary>
    public class OsuEditorInformation
    {
        /// <summary>
        /// List of bookmarks, placed using time in milliseconds.
        /// </summary>
        public List<int> Bookmarks { get; set; }
        
        /// <summary>
        /// The multiplier for distance snapping.
        /// </summary>
        public decimal DistanceSpacing { get; set; }
        
        /// <summary>
        /// The divisor to use for beat snapping.
        /// </summary>
        public decimal BeatDivisor { get; set; }
        
        /// <summary>
        /// The size of the editor grid.
        /// </summary>
        public int GridSize { get; set; }
        
        /// <summary>
        /// Scale factor to use for the object timeline.
        /// </summary>
        public int TimelineZoom { get; set; }
    }
}