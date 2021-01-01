using OpenChart.Charting.Properties;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for a BeatDuration object.
    /// </summary>
    public class BeatDurationConverter : JsonConverter<BeatDuration>
    {
        public override BeatDuration ReadJson(JsonReader reader, Type objectType, BeatDuration existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var val = new JValue(reader.Value);

            switch (val.Type)
            {
                case JTokenType.Float:
                case JTokenType.Integer:
                    return new BeatDuration((double)val);
                case JTokenType.Null:
                    throw new ConverterException("Beat duration cannot be null.");
                default:
                    throw new ConverterException("Beat duration must be a number.");
            }
        }

        public override void WriteJson(JsonWriter writer, BeatDuration value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
