using OpenChart.Charting;
using System.IO;
using System.Text;
using System.Text.Json;

namespace OpenChart.Formats.OpenChart
{
    public class OpenChartFileHandler : IFileHandler<Chart>
    {
        public class FileMetadata
        {
            public int KeyCount { get; set; }
            public string Version { get; set; }
            public string Author { get; set; }
            public string ChartName { get; set; }
        }

        public class FileData
        {
            public FileMetadata Metadata { get; set; }
        }

        static JsonSerializerOptions jsonOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string Version => "0.1";

        /// <summary>
        /// The OpenChart file extension.
        /// </summary>
        public static string FileExtension => ".oc";

        /// <summary>
        /// Reads the data from an OpenChart file and returns a Chart object. Does not close the stream.
        /// </summary>
        /// <param name="stream">The stream to read from (typically a FileStream).</param>
        public Chart Read(StreamReader stream)
        {
            var fd = LoadFileData(stream);
            var chart = new Chart(fd.Metadata.KeyCount);

            return chart;
        }

        /// <summary>
        /// Writes the chart to the provided stream. Does not close the stream.
        /// </summary>
        /// <param name="chart">The chart object.</param>
        /// <param name="stream">The stream to write to (typically a FileStream).</param>
        public void Write(Chart chart, StreamWriter stream)
        {
            var fd = ConvertChartToFileData(chart);
            stream.Write(JsonSerializer.Serialize(fd, jsonOptions));
            stream.Flush();
        }

        public FileData ConvertChartToFileData(Chart chart)
        {
            var fd = new FileData();

            fd.Metadata = new FileMetadata();
            fd.Metadata.KeyCount = chart.KeyCount;
            fd.Metadata.Version = "0.1";

            return fd;
        }

        public FileData LoadFileData(StreamReader stream)
        {
            var data = stream.ReadToEnd();
            var fd = JsonSerializer.Deserialize(
                Encoding.UTF8.GetBytes(data),
                typeof(FileData),
                jsonOptions
            );

            return (FileData)fd;
        }
    }
}