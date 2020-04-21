using OpenChart.Charting.Properties;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenChart.Formats.OpenChart.Version0_1.Converters
{
    /// <summary>
    /// JSON converter for a Key object.
    /// </summary>
    public class KeyIndexConverter : JsonConverter<KeyIndex>
    {
        public override KeyIndex Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new KeyIndex(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, KeyIndex value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }
}
