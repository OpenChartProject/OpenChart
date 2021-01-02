using OpenChart.Charting.Properties;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for a Key object.
    /// </summary>
    public class KeyIndexConverter : JsonConverter<KeyIndex>
    {
        public override KeyIndex ReadJson(JsonReader reader, Type objectType, KeyIndex existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var val = new JValue(reader.Value);

            switch (val.Type)
            {
                case JTokenType.Integer:
                    return new KeyIndex((int)val);
                case JTokenType.Null:
                    throw new ConverterException("Key index cannot be null.");
                default:
                    throw new ConverterException("Key index must be an integer.");
            }
        }

        public override void WriteJson(JsonWriter writer, KeyIndex value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
