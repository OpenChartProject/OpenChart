using OpenChart.Charting.Properties;
using System;
using Newtonsoft.Json;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for a Beat object.
    /// </summary>
    public class BeatConverter : JsonConverter<Beat>
    {
        public override Beat ReadJson(JsonReader reader, Type objectType, Beat existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new Beat((double)reader.ReadAsDouble());
        }

        public override void WriteJson(JsonWriter writer, Beat value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
