namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// The data that is saved to/loaded from OpenChart files.
    /// </summary>
    public class FileData
    {
        /// <summary>
        /// The chart's metadata.
        /// </summary>
        public FileMetadata Metadata { get; set; }
    }
}
