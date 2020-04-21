using OpenChart.Charting.Properties;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenChart.Formats.OpenChart.Version0_1.Converters
{
    /// <summary>
    /// JSON converter for a Beat object.
    /// </summary>
    public class BeatConverter : JsonConverter<Beat>
    {
        public override Beat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Beat(reader.GetDouble());
        }

        public override void Write(Utf8JsonWriter writer, Beat value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }
}
