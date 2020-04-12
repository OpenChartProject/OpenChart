using OpenChart.Charting;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// A converter for transforming Chart objects into FileData objects and vice versa.
    /// </summary>
    public class OpenChartConverter : IFormatConverter<FileData>
    {
        /// <summary>
        /// Converts a format-specific chart to a native Chart.
        /// </summary>
        /// <param name="fd">A FileData object.</param>
        public Chart ToNative(FileData fd)
        {
            var chart = new Chart(fd.Metadata.KeyCount);

            return chart;
        }

        /// <summary>
        /// Converts a native Chart to a FileData object.
        /// </summary>
        /// <param name="chart">A native Chart.</param>
        public FileData FromNative(Chart chart)
        {
            var fd = new FileData();

            fd.Metadata = new FileMetadata();
            fd.Metadata.KeyCount = chart.KeyCount;
            fd.Metadata.Version = "0.1";

            return fd;
        }
    }
}