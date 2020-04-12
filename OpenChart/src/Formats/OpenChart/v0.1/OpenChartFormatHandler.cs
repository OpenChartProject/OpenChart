using OpenChart.Charting;
using System.IO;
using System.Text;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    public class OpenChartFormatHandler : IFormatHandler
    {
        static IFormatConverter<FileData> converter = new OpenChartConverter();
        static IFormatSerializer<FileData> serializer = new OpenChartSerializer();

        /// <summary>
        /// The name of the OpenChart format.
        /// </summary>
        public string FormatName => "OpenChart";

        /// <summary>
        /// The extension used by the OpenChart format.
        /// </summary>
        public string FileExtension => ".oc";

        /// <summary>
        /// Reads data from an OpenChart file and returns a Chart object.
        /// </summary>
        /// <param name="reader">The stream that contains the file data.</param>
        public Chart Read(StreamReader reader)
        {
            var data = reader.ReadToEnd();
            var fd = serializer.Deserialize(Encoding.UTF8.GetBytes(data));

            return converter.ToNative(fd);
        }

        /// <summary>
        /// Writes the chart object to an OpenChart file.
        /// </summary>
        /// <param name="writer">The stream to write to.</param>
        /// <param name="chart">The chart to write.</param>
        public void Write(StreamWriter writer, Chart chart)
        {
            var fd = converter.FromNative(chart);
            var data = serializer.Serialize(fd);

            writer.Write(data);
            writer.Flush();
        }
    }
}