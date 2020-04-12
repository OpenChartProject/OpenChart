using OpenChart.Formats.Osu.Enum;

namespace OpenChart.Formats.Osu.Data
{
    /// <summary>
    /// General information about an osu! beatmap.
    /// </summary>
    public class OsuGeneralInformation
    {
        /// <summary>
        /// The path to the audio file, relative to the location of the .osu file.
        /// </summary>
        public string AudioFilename { get; set; }
        
        /// <summary>
        /// The number of milliseconds to wait before audio should start playing.
        /// </summary>
        public int AudioLeadIn { get; set; }
        
        /// <summary>
        /// Deprecated.
        /// TODO?: figure out what this field does, as the documentation is missing for this
        /// </summary>
        public string AudioHash { get; set; }
        
        /// <summary>
        /// Timestamp, in milliseconds, for when the audio preview should begin.
        /// </summary>
        public int PreviewTime { get; set; }
        
        /// <summary>
        /// The speed of the countdown at the start of the map, and whether it should be enabled.
        /// </summary>
        public OsuCountdown Countdown { get; set; }
        
        /// <summary>
        /// The hitsound sample set to use by default, if timing points do not override it.
        /// </summary>
        public OsuSampleSet SampleSet { get; set; }
        
        /// <summary>
        /// Multiplier for the threshold in time where objects that are placed close to one another stack.
        /// Not used in modes other than osu!.
        /// </summary>
        public decimal StackLeniency { get; set; }

        /// <summary>
        /// The game mode for this difficulty.
        /// </summary>
        public OsuGameMode Mode { get; set; }

        /// <summary>
        /// Whether a letterbox effect should be displayed during beatmap breaks.
        /// </summary>
        public bool LetterboxInBreaks { get; set; }
        
        /// <summary>
        /// Deprecated.
        /// TODO?: figure out what this field does, as the documentation is missing for this
        /// </summary>
        public bool StoryFireInFront { get; set; }
        
        /// <summary>
        /// Whether the storyboard can use sprites contained within the user's active skin.
        /// </summary>
        public bool UseSkinSprites { get; set; }
        
        /// <summary>
        /// Deprecated.
        /// TODO?: figure out what this field does, as the documentation is missing for this
        /// </summary>
        public bool AlwaysShowPlayfield { get; set; }

        /// <summary>
        /// Whether to draw hit circle overlays over hit numbers.
        /// </summary>
        public OsuOverlayPosition OverlayPosition { get; set; }
        
        /// <summary>
        /// The name of the preferred skin for this beatmap. This can be overridden by game client settings.
        /// </summary>
        public string SkinPreference { get; set; }
        
        /// <summary>
        /// Whether to display an epilepsy warning at the start of the beatmap.
        /// </summary>
        public bool EpilepsyWarning { get; set; }
        
        /// <summary>
        /// Time, in beats, that the countdown starts at before the first hit object.
        /// </summary>
        public int CountdownOffset { get; set; }
        
        /// <summary>
        /// For osu!mania, whether to use the special N+1 key layout.
        /// </summary>
        public bool SpecialStyle { get; set; }
        
        /// <summary>
        /// Whether the scoreboard allows widescreen display.
        /// </summary>
        public bool WidescreenStoryboard { get; set; }
        
        /// <summary>
        /// Whether sound samples change playback rate when playing with speed mods (HT/DT).
        /// </summary>
        public bool SamplesMatchPlaybackRate { get; set; }
    }
}