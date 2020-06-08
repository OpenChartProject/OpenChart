namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents the song data in a SM file. This is usually the first data defined in the file.
    /// </summary>
    public class SongData
    {
        /// <summary>
        /// The title of the song.
        /// Field: #TITLE
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The subtitle of the song.
        /// Field: #SUBTITLE
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// The artist of the song.
        /// Field: #ARTIST
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// The transliterated title of the song (e.g. jp -> romaji).
        /// Field: #TITLETRANSLIT
        /// </summary>
        public string TransliteratedTitle { get; set; }

        /// <summary>
        /// The transliterated subtitle of the song (e.g. jp -> romaji).
        /// Field: #SUBTITLETRANSLIT
        /// </summary>
        public string TransliteratedSubtitle { get; set; }

        /// <summary>
        /// The transliterated artist of the song (e.g. jp -> romaji).
        /// Field: #ARTISTTRANSLIT
        /// </summary>
        public string TransliteratedArtist { get; set; }

        /// <summary>
        /// The genre of the song.
        /// Field: #GENRE
        /// </summary>
        public string Genre { get; set; }
    }
}
