using System.Collections.Generic;

namespace OpenChart.Formats.Osu.Data
{
    public class OsuBeatmapMetadata
    {
        /// <summary>
        /// The romanized song title.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// The song title, containing Unicode characters.
        /// </summary>
        public string TitleUnicode { get; set; }
        
        /// <summary>
        /// The romanized song artist.
        /// </summary>
        public string Artist { get; set; }
        
        /// <summary>
        /// The song artist, containing Unicode characters.
        /// </summary>
        public string ArtistUnicode { get; set; }
        
        /// <summary>
        /// The username of the beatmapper.
        /// </summary>
        public string Creator { get; set; }
        
        /// <summary>
        /// The name of this difficulty.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// The source of the song (album, etc.)
        /// </summary>
        public string Source { get; set; }
        
        /// <summary>
        /// A list of tags for search engines to find this beatmap.
        /// </summary>
        public List<string> Tags { get; set; }
        
        /// <summary>
        /// The beatmap ID assigned to this difficulty upon submission.
        /// </summary>
        public int BeatmapId { get; set; }
        
        /// <summary>
        /// The beatmap set ID assigned to this beatmap set upon submission.
        /// </summary>
        public int BeatmapSetId { get; set; }
    }
}