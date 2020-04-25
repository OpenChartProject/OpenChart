namespace OpenChart.Songs
{
    /// <summary>
    /// Metadata about a song.
    /// </summary>
    public class SongMetadata
    {
        /// <summary>
        /// The artist of the song.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// The path of the audio file for this song.
        /// </summary>
        public string AudioFilePath { get; set; }

        /// <summary>
        /// The title of the song.
        /// </summary>
        public string Title { get; set; }
    }
}
