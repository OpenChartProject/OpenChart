using OpenChart.Charting;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// Metadata about the chart that is saved.
    /// </summary>
    public class FileMetadata
    {
        /// <summary>
        /// The chart's keycount.
        /// </summary>
        public KeyCount KeyCount { get; set; }

        /// <summary>
        /// The version of the file format.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The name of the chart's author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The chart's name.
        /// </summary>
        public string ChartName { get; set; }
    }
}
