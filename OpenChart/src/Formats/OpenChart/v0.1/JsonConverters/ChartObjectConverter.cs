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
    ///
    /// This class acts like a factory for converting the concrete chart object types.
    /// Each IChartObject class has a 'type' field which says what kind of object it is.
    /// The chart objects themselves aren't complex enough to warrant having their own
    /// converter.
    ///
    /// Every chart object is represented as a JSON object ({}). This reads in the full
    /// object and only looks at the value of the 'type' field. Once we know the type of
    /// the object we can deserialize it into an object instance.
    ///
    /// In a sense, what this converter is doing is taking the full JSON text for the file,
    /// extracting a small part of it, and then deserializing that part as if it were its
    /// own file.
    /// </summary>
    public class ChartObjectConverter : JsonConverter<IChartObject>
    {
        public override IChartObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new ConverterException("Chart object must be a JSON object.");
            }

            // Read the entire JSON object.
            JsonDocument document = JsonDocument.ParseValue(ref reader);
            string typeString;

            try
            {
                // Try looking up the 'type' field.
                typeString = document.RootElement.GetProperty("type").GetString();
            }
            catch (KeyNotFoundException)
            {
                throw new ConverterException("Chart object must contain a 'type' field.");
            }

            using (var stream = new MemoryStream())
            {
                // The .NET JSON converters don't work on already parsed objects, so unfortunately
                // we can't just pass the document instance directly to a converter.
                using (var writer = new Utf8JsonWriter(stream))
                {
                    document.WriteTo(writer);
                }

                var json = Encoding.UTF8.GetString(stream.ToArray());

                // Deserializes the object based on its type.
                switch (typeString)
                {
                    case ChartObjectType.TapNote:
                        return JsonSerializer.Deserialize<TapNote>(json, options);
                    case ChartObjectType.HoldNote:
                        return JsonSerializer.Deserialize<HoldNote>(json, options);
                    default:
                        throw new ConverterException($"Unknown chart object type: '{typeString}'");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, IChartObject value, JsonSerializerOptions options)
        {
            // Serializes the object based on its type.
            if (value is TapNote)
            {
                JsonSerializer.Serialize<TapNote>(writer, (TapNote)value, options);
            }
            else if (value is HoldNote)
            {
                JsonSerializer.Serialize<HoldNote>(writer, (HoldNote)value, options);
            }
            else
            {
                throw new ConverterException("Cannot serialize chart object, type is unknown.");
            }
        }
    }
}