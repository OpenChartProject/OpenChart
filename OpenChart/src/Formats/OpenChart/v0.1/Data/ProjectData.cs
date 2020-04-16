namespace OpenChart.Formats.OpenChart.Version0_1.Data
{
    /// <summary>
    /// The data that is saved to/loaded from OpenChart files.
    /// </summary>
    public class ProjectData
    {
        /// <summary>
        /// Metadata about the project or file format.
        /// </summary>
        public ProjectMetadata Metadata { get; set; }

        /// <summary>
        /// The song data. Can be null if there is no song.
        /// </summary>
        public SongData Song { get; set; }

        /// <summary>
        /// The chart(s) for this project.
        /// </summary>
        public ChartData[] Charts { get; set; }

        public ProjectData()
        {
            Metadata = new ProjectMetadata();
        }
    }
}