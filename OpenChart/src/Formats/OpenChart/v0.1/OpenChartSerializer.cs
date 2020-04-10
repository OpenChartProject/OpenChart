using System.Text;
using System.Text.Json;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// Serializer for the OpenChart format. Uses JSON to serialize/unserialize FileData objects.
    /// </summary>
    public class OpenChartSerializer : IFormatSerializer<FileData>
    {
        static JsonSerializerOptions jsonOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Unserializes raw JSON data into a FileData object.
        /// </summary>
        /// <param name="data">JSON data.</param>
        public FileData Unserialize(byte[] data)
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