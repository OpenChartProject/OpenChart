using OpenChart.Charting.Properties;
using System;
using Newtonsoft.Json;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for a Key object.
    /// </summary>
    public class KeyIndexConverter : JsonConverter<KeyIndex>
    {
        public override KeyIndex ReadJson(JsonReader reader, Type objectType, KeyIndex existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new KeyIndex((int)reader.ReadAsInt32());
        }

        public override void WriteJson(JsonWriter writer, KeyIndex value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
