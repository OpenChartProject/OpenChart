using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenChart.Formats.OpenChart.Version0_1.Converters
{
    /// <summary>
    /// JSON converter factory for reading and writing IChartObjects.
    /// </summary>
    public class ChartObjectConverterFactory : JsonConverter<IChartObject>
    {
        public override IChartObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, IChartObject value, JsonSerializerOptions options)
        {
            //writer.WriteNull();
        }
    }
}