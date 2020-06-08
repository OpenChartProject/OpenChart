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

        /// <summary>
        /// The path to the audio file.
        /// Field: #MUSIC
        /// </summary>
        public string Music { get; set; }

        /// <summary>
        /// The path to the lyrics file. (not supported)
        /// Field: #LYRICSPATH
        /// </summary>
        public string LyricsPath { get; set; }

        /// <summary>
        /// The start time of the audio sample to play on the music wheel.
        /// Field: #SAMPLESTART
        /// </summary>
        public double SampleStart { get; set; }

        /// <summary>
        /// The length of the audio sample to play on the music wheel.
        /// Field: #SAMPLELENGTH
        /// </summary>
        public double SampleLength { get; set; }
    }
}
