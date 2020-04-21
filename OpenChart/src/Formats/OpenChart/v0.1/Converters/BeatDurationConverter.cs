using OpenChart.Charting.Properties;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenChart.Formats.OpenChart.Version0_1.Converters
{
    /// <summary>
    /// JSON converter for a BeatDuration object.
    /// </summary>
    public class BeatDurationConverter : JsonConverter<BeatDuration>
    {
        public override BeatDuration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new BeatDuration(reader.GetDouble());
        }

        public override void Write(Utf8JsonWriter writer, BeatDuration value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }
}
