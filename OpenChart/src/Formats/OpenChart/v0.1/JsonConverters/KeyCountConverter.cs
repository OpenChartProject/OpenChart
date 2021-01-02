using OpenChart.Charting.Properties;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for a KeyCount object.
    /// </summary>
    public class KeyCountConverter : JsonConverter<KeyCount>
    {
        public override KeyCount ReadJson(JsonReader reader, Type objectType, KeyCount existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var val = new JValue(reader.Value);

            switch (val.Type)
            {
                case JTokenType.Integer:
                    return new KeyCount((int)val);
                case JTokenType.Null:
                    throw new ConverterException("Key count cannot be null.");
                default:
                    throw new ConverterException("Key count must be an integer.");
            }
        }

        public override void WriteJson(JsonWriter writer, KeyCount value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
