using System.Text;
using System.Text.Json;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// Serializer for the OpenChart format. Uses JSON to serialize/deserialize FileData objects.
    /// </summary>
    public class OpenChartSerializer : IFormatSerializer<FileData>
    {
        static JsonSerializerOptions jsonOptions;

        static OpenChartSerializer()
        {
            jsonOptions = new JsonSerializerOptions();

            jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.Converters.Add(new BeatConverter());
            jsonOptions.Converters.Add(new BeatDurationConverter());
            jsonOptions.Converters.Add(new KeyConverter());
            jsonOptions.Converters.Add(new KeyCountConverter());
        }

        /// <summary>
        /// Deserializes raw JSON data into a FileData object.
        /// </summary>
        /// <param name="data">JSON data.</param>
        public FileData Deserialize(byte[] data)
        {
            return (FileData)JsonSerializer.Deserialize(data, typeof(FileData), jsonOptions);
        }

        /// <summary>
        /// Serializes a FileData object into JSON.
        /// </summary>
        /// <param name="fd">The FileData object.</param>
        public byte[] Serialize(FileData fd)
        {
            var str = JsonSerializer.Serialize(fd, jsonOptions);

            return Encoding.UTF8.GetBytes(str);
        }
    }
}