using System;

namespace OpenChart.Formats.OpenChart.Version0_1.Data
{
    public class SongData
    {
        /// <summary>
        /// The artist of the song.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// The title of the song.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A path to the audio file.
        /// </summary>
        public string Path { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SongData data)
                return (
                    Artist == data.Artist &&
                    Title == data.Title &&
                    Path == data.Path
                );

            return false;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Artist, Title, Path).GetHashCode();
        }
    }
}
