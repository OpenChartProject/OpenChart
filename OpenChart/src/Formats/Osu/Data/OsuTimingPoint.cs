using OpenChart.Formats.Osu.Enum;

namespace OpenChart.Formats.Osu.Data
{
    /// <summary>
    /// A timing point for an osu! beatmap. These are often used to signify sections of the beatmap, or to control the timing of a specific section.
    /// </summary>
    public class OsuTimingPoint
    {
        /// <summary>
        /// The start point of the timing section, in milliseconds from the start of the beatmap's audio.
        /// </summary>
        public int Time { get; set; }
        
        /// <summary>
        /// This property can mean two things:
        /// - For uninherited timing points, the length of the timing section, in milliseconds.
        /// - For inherited timing points, a negative inverse slider velocity multiplier, as a percentage. For example,
        ///   a value of -50 would make all sliders in this timing section twice as fast as the value defined in
        ///   <see cref="OsuDifficultyInformation.SliderMultiplier"/>.
        /// </summary>
        public decimal BeatLength { get; set; }
        
        /// <summary>
        /// The number of beats in a measure. Ignored in inherited timing points.
        /// </summary>
        public int Meter { get; set; }
        
        /// <summary>
        /// The default sample set to use for hit objects in this timing section. 
        /// </summary>
        public OsuPredefinedSampleSet SampleSet { get; set; }
        
        /// <summary>
        /// The index to use for custom hit object samples. Set to 0 to use osu!'s default hitsounds.
        /// </summary>
        public int SampleIndex { get; set; }
        
        /// <summary>
        /// The volume of hit objects in this timing section, as a percentage (0-100).
        /// </summary>
        public int Volume { get; set; }
        
        /// <summary>
        /// The effects to apply to this timing section.
        /// </summary>
        public OsuTimingPointEffects Effects { get; set; }
    }
}