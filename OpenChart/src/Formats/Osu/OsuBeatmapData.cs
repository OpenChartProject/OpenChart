using System.Collections.Generic;
using OpenChart.Formats.Osu.Data;

namespace OpenChart.Formats.Osu
{
    /// <summary>
    /// The data that is saved to/loaded from osu! beatmap files.
    /// </summary>
    /// TODO: Implement color support.
    public class OsuBeatmapData
    {
        /// <summary>
        /// The version of the osu! file format.
        /// Found at the top of the .osu file as a string:
        /// osu file format v14
        /// </summary>
        public string FormatVersion { get; set; }
        
        /// <summary>
        /// General information about the beatmap, such as the path to the audio file, and information used by the client
        /// to set up the playfield.
        /// </summary>
        public OsuGeneralInformation GeneralInformation { get; set; }
        
        /// <summary>
        /// Information used by beatmap editors for an osu! beatmap.
        /// </summary>
        public OsuEditorInformation EditorInformation { get; set; }
        
        /// <summary>
        /// Beatmap metadata, contains information about the song as well as beatmap and beatmap set IDs.
        /// </summary>
        public OsuBeatmapMetadata BeatmapMetadata { get; set; }

        /// <summary>
        /// Contains information about the difficulty of a beatmap.
        /// </summary>
        public OsuDifficultyInformation DifficultyInformation { get; set; }
        
        /// <summary>
        /// Beatmap events, used to signal events to the client (breaks, new backgrounds etc.)
        /// </summary>
        public List<OsuBeatmapEvent> BeatmapEvents { get; set; }
        
        /// <summary>
        /// Timing points. Used to define distinct sections of a beatmap, or to adjust timing.
        /// </summary>
        public List<OsuTimingPoint> TimingPoints { get; set; }
        
        /// <summary>
        /// Hit objects. Used to define the actual hit points on the beatmap.
        /// </summary>
        public List<OsuHitObject> HitObjects { get; set; }
    }
}