using OpenChart.Charting.Properties;
using System;
using Newtonsoft.Json;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for a KeyCount object.
    /// </summary>
    public class KeyCountConverter : JsonConverter<KeyCount>
    {
        public override KeyCount ReadJson(JsonReader reader, Type objectType, KeyCount existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new KeyCount((int)reader.ReadAsInt32());
        }

        public override void WriteJson(JsonWriter writer, KeyCount value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
