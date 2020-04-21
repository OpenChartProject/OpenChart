using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Formats.OpenChart.Version0_1.Converters;
using System.Text;
using System.Text.Json;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// Serializer for the OpenChart format. Uses JSON to serialize/deserialize FileData objects.
    /// </summary>
    public class OpenChartSerializer : IFormatSerializer<ProjectData>
    {
        static JsonSerializerOptions jsonOptions;

        static OpenChartSerializer()
        {
            jsonOptions = new JsonSerializerOptions();

            jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.Converters.Add(new BeatConverter());
            jsonOptions.Converters.Add(new BeatDurationConverter());
            jsonOptions.Converters.Add(new KeyIndexConverter());
            jsonOptions.Converters.Add(new KeyCountConverter());
        }

        /// <summary>
        /// Deserializes raw JSON data into a FileData object.
        /// </summary>
        /// <param name="data">JSON data.</param>
        public ProjectData Deserialize(byte[] data)
        {
            var pd = (ProjectData)JsonSerializer.Deserialize(data, typeof(ProjectData), jsonOptions);

            if (pd.Metadata == null)
            {
                throw new SerializerException("The 'metadata' is missing or null.");
            }
            else if (string.IsNullOrEmpty(pd.Metadata.Version))
            {
                throw new SerializerException("The 'version' field is missing or empty.");
            }

            if (pd.Charts == null)
            {
                pd.Charts = new ChartData[] { };
            }

            return pd;
        }

        /// <summary>
        /// Serializes a FileData object into JSON.
        /// </summary>
        /// <param name="fd">The FileData object.</param>
        public byte[] Serialize(ProjectData pd)
        {
            var str = JsonSerializer.Serialize(pd, jsonOptions);

            return Encoding.UTF8.GetBytes(str);
        }
    }
}