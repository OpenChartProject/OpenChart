using OpenChart.Charting;
using OpenChart.Songs;
using System.Collections.Generic;

namespace OpenChart.Projects
{
    /// <summary>
    /// A project represents a collection of one or more charts along with the song metadata
    /// (if there is a song).
    /// </summary>
    public class Project
    {
        /// <summary>
        /// A list of charts for this project.
        /// </summary>
        public List<Chart> Charts { get; private set; }

        /// <summary>
        /// The metadata for the song. Can be null.
        /// </summary>
        public SongMetadata SongMetadata { get; set; }

        /// <summary>
        /// Creates a new Project instance.
        /// </summary>
        public Project()
        {
            Charts = new List<Chart>();
            SongMetadata = null;
        }
    }
}