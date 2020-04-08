using OpenChart.Charting;
using System.IO;
using System.Text.Json;

namespace OpenChart.Formats.OpenChart
{
    public class OpenChartFileHandler : IFileHandler<Chart>
    {
        struct FileMetaData
        {
            public string version;
            public string author;
            public string chartName;
        }

        struct FileData
        {
            public FileMetaData metaData;
        }

        public static string Version => "0.1";

        /// <summary>
        /// The OpenChart file extension.
        /// </summary>
        public static string FileExtension => ".oc";

        /// <summary>
        /// Reads the data from an OpenChart file and returns a Chart object.
        /// </summary>
        /// <param name="stream">The stream to read from (typically a FileStream).</param>
        public Chart Read(StreamReader stream)
        {
            return null;
        }

        /// <summary>
        /// Writes the chart to the provided stream.
        /// </summary>
        /// <param name="chart">The chart object.</param>
        /// <param name="stream">The stream to write to (typically a FileStream).</param>
        public void Write(Chart chart, StreamWriter stream)
        {
            var fd = new FileData();
            fd.metaData = new FileMetaData
            {
                author = "Jesse",
                chartName = "Test",
                version = Version
            };

            stream.Write(JsonSerializer.Serialize(fd));
        }
    }
}