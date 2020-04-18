using OpenChart.Formats.Osu.Enum;

namespace OpenChart.Formats.Osu.Data
{
    /// <summary>
    /// Information about custom hit samples for hit objects.
    /// </summary>
    public class OsuCustomHitSample
    {
        /// <summary>
        /// The sample set of the normal sound.
        /// </summary>
        public OsuPredefinedSampleSet NormalSampleSet { get; set; }
        
        /// <summary>
        /// The sample set of the finish, whistle and clap sounds.
        /// </summary>
        public OsuPredefinedSampleSet AdditionalSet { get; set; }
        
        /// <summary>
        /// The index of the sample. If set to 0, the timing point's sample index will be used instead.
        /// </summary>
        public int Index { get; set; }
        
        /// <summary>
        /// The volume of the sample. If set to 0, the timing point's volume will be used instead.
        /// </summary>
        public int Volume { get; set; }
        
        /// <summary>
        /// The filename of the sample, relative to the .osu file.
        /// </summary>
        public string Filename { get; set; }
    }
}