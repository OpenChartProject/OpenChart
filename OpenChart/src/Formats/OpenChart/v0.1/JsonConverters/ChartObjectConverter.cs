using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    /// <summary>
    /// JSON converter for reading and writing objects that inherit from IChartObject.
    /// </summary>
    public class ChartObjectConverter : JsonConverter<IChartObject>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(IChartObject).IsAssignableFrom(typeToConvert);
        }

        public override IChartObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var objectType = reader.GetString();

                switch (objectType)
                {
                    case TapNote.Type:
                        return new TapNote();
                    case HoldNote.Type:
                        throw new ConverterException("Hold notes are not a simple chart object type.");
                    default:
                        throw new ConverterException($"Unexpected chart object type: '{objectType}'");
                }
            }
            else if (reader.TokenType == JsonTokenType.StartArray)
            {
                reader.Read();

                if (reader.TokenType != JsonTokenType.String)
                {
                    throw new ConverterException("Expected a string as the first parameter for an extended chart object.");
                }

                var objectType = reader.GetString();

                reader.Read();

                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new ConverterException("Expected a JSON object as the second parameter for an extended chart object.");
                }

                reader.Read();

                switch (objectType)
                {
                    case TapNote.Type:
                        throw new ConverterException("Tap notes are not an extended chart object type.");
                    case HoldNote.Type:
                        var note = new HoldNote();
                        return note;
                    default:
                        throw new ConverterException($"Unexpected chart object type: '{objectType}'");
                }
            }
            else
            {
                throw new ConverterException("Expected string, array, or null.");
            }
        }

        public override void Write(Utf8JsonWriter writer, IChartObject value, JsonSerializerOptions options)
        {
            //writer.WriteNull();
        }
    }
}