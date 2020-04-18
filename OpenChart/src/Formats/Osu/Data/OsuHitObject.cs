using System.Collections.Generic;

namespace OpenChart.Formats.Osu.Data
{
    /// <summary>
    /// A standard hit object for an osu! beatmap.
    /// </summary>
    public class OsuHitObject
    {
        /// <summary>
        /// The X position of the object, in osu!pixels.
        /// In osu!mania, this is used to define the column which this hit object should appear in.
        /// In osu!catch, this is used to determine how far across the screen 
        /// In osu!taiko, this field is ignored.
        /// </summary>
        public int XPosition { get; set; }
        
        /// <summary>
        /// The Y position of the object, in osu!pixels.
        /// In all game modes except osu!standard, this field is ignored.
        /// </summary>
        public int YPosition { get; set; }
        
        /// <summary>
        /// The time, in milliseconds, from the start of the beatmap's audio, when this object should appear.
        /// </summary>
        public int Time { get; set; }
        
        /// <summary>
        /// Extra type data for an osu! hit object. Contains information about colorhax and combo splitting.
        /// </summary>
        public OsuExtraTypeData ExtraTypeData { get; set; }
        
        /// <summary>
        /// Information about which hitsound samples to play for this hit object.
        /// </summary>
        public OsuHitObjectSample HitSounds { get; set; }
        
        /// <summary>
        /// Arbitrary hit object parameters.
        /// </summary>
        public List<OsuHitObjectParameter> ObjectParameters { get; set; }
        
        /// <summary>
        /// Custom hit sample data for this hit object.
        /// </summary>
        public OsuCustomHitSample HitSample { get; set; }
    }
}