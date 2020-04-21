using OpenChart.Charting.Properties;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenChart.Formats.OpenChart.Version0_1.Converters
{
    /// <summary>
    /// JSON converter for a KeyCount object.
    /// </summary>
    public class KeyCountConverter : JsonConverter<KeyCount>
    {
        public override KeyCount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new KeyCount(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, KeyCount value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }
}
