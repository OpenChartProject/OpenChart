namespace OpenChart.Formats.Osu.Data
{
    public class OsuHitObjectSample
    {
        /// <summary>
        /// Whether to play normal hit sound for this hit object.
        /// </summary>
        public bool Normal { get; set; }
        
        /// <summary>
        /// Whether to play whistle hit sound for this hit object.
        /// </summary>
        public bool Whistle { get; set; }
        
        /// <summary>
        /// Whether to play finish hit sound for this hit object.
        /// </summary>
        public bool Finish { get; set; }
        
        /// <summary>
        /// Whether to play clap hit sound for this hit object.
        /// </summary>
        public bool Clap { get; set; }
    }
}