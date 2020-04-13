using OpenChart.Formats.OpenChart.Version0_1.Data;
using System.Text;
using System.Text.Json;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// Serializer for the OpenChart format. Uses JSON to serialize/deserialize FileData objects.
    /// </summary>
    public class OpenChartSerializer : IFormatSerializer<ProjectData>
    {
        static JsonSerializerOptions jsonOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Deserializes raw JSON data into a FileData object.
        /// </summary>
        /// <param name="data">JSON data.</param>
        public ProjectData Deserialize(byte[] data)
        {
            return (ProjectData)JsonSerializer.Deserialize(data, typeof(ProjectData), jsonOptions);
        }

        /// <summary>
        /// Serializes a FileData object into JSON.
        /// </summary>
        /// <param name="fd">The FileData object.</param>
        public byte[] Serialize(ProjectData fd)
        {
            var str = JsonSerializer.Serialize(fd, jsonOptions);

            return Encoding.UTF8.GetBytes(str);
        }
    }
}