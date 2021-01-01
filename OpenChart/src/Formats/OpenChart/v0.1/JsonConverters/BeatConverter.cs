using OpenChart.Charting.Properties;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for a Beat object.
    /// </summary>
    public class BeatConverter : JsonConverter<Beat>
    {
        public override Beat ReadJson(JsonReader reader, Type objectType, Beat existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var val = new JValue(reader.Value);

            switch (val.Type)
            {
                case JTokenType.Float:
                case JTokenType.Integer:
                    return new Beat((double)val);
                case JTokenType.Null:
                    throw new ConverterException("Beat cannot be null.");
                default:
                    throw new ConverterException("Beat must be a number.");
            }
        }

        public override void WriteJson(JsonWriter writer, Beat value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
