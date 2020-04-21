using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for reading and writing objects that inherit from IChartObject.
    /// </summary>
    public class ChartObjectConverter : JsonConverter<IChartObject>
    {
        public override IChartObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new ConverterException("Chart object must be a JSON object.");
            }

            JsonDocument document = JsonDocument.ParseValue(ref reader);
            string typeString;

            try
            {
                typeString = document.RootElement.GetProperty("type").GetString();
            }
            catch (KeyNotFoundException)
            {
                throw new ConverterException("Chart object must contain a 'type' field.");
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    document.WriteTo(writer);
                }

                var json = Encoding.UTF8.GetString(stream.ToArray());

                switch (typeString)
                {
                    case TapNote.Type:
                        return JsonSerializer.Deserialize<TapNote>(json, options);
                    case HoldNote.Type:
                        return JsonSerializer.Deserialize<HoldNote>(json, options);
                    default:
                        throw new ConverterException($"Unknown chart object type: '{typeString}'");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, IChartObject value, JsonSerializerOptions options)
        {
            //writer.WriteNull();
        }
    }
}