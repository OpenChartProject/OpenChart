using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Formats.OpenChart.Version0_1.JsonConverters;
using System.Text;
using System.Text.Json;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// Serializer for the OpenChart format. Uses JSON to serialize/deserialize FileData objects.
    /// </summary>
    public class OpenChartSerializer : IFormatSerializer<ProjectData>
    {
        /// <summary>
        /// The options used by the serializer.
        /// </summary>
        public static JsonSerializerOptions JsonOptions;

        static OpenChartSerializer()
        {
            JsonOptions = new JsonSerializerOptions();

            JsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            JsonOptions.Converters.Add(new BeatConverter());
            JsonOptions.Converters.Add(new BeatDurationConverter());
            JsonOptions.Converters.Add(new KeyIndexConverter());
            JsonOptions.Converters.Add(new KeyCountConverter());
            JsonOptions.Converters.Add(new ChartObjectConverter());
        }

        /// <summary>
        /// Deserializes raw JSON data into a FileData object.
        /// </summary>
        /// <param name="data">JSON data.</param>
        public ProjectData Deserialize(byte[] data)
        {
            return (ProjectData)JsonSerializer.Deserialize(data, typeof(ProjectData), JsonOptions);
        }

        /// <summary>
        /// Serializes a FileData object into JSON.
        /// </summary>
        /// <param name="fd">The FileData object.</param>
        public byte[] Serialize(ProjectData pd)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(pd, JsonOptions));
        }
    }
}
