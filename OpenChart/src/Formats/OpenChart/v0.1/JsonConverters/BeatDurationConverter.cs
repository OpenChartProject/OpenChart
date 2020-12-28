using OpenChart.Charting.Properties;
using System;
using Newtonsoft.Json;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for a BeatDuration object.
    /// </summary>
    public class BeatDurationConverter : JsonConverter<BeatDuration>
    {
        public override BeatDuration ReadJson(JsonReader reader, Type objectType, BeatDuration existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new BeatDuration((double)reader.ReadAsDouble());
        }

        public override void WriteJson(JsonWriter writer, BeatDuration value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
