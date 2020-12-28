using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Formats.OpenChart.Version0_1.JsonConverters;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        public static JsonSerializerSettings JsonOptions;

        static OpenChartSerializer()
        {
            JsonOptions = new JsonSerializerSettings();

            JsonOptions.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
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
            var str = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<ProjectData>(str, JsonOptions);
        }

        /// <summary>
        /// Serializes a ProjectData object into JSON.
        /// </summary>
        /// <param name="pd">The ProjectData object.</param>
        public byte[] Serialize(ProjectData pd)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pd, JsonOptions));
        }
    }
}
