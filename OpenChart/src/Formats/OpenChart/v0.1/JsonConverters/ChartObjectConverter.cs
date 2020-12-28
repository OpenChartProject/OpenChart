using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace OpenChart.Formats.OpenChart.Version0_1.JsonConverters
{
    class ChartTypeField
    {
        public string Type { get; set; }
    }

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
        public override IChartObject ReadJson(JsonReader reader, Type objectType, IChartObject existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new ConverterException("Chart object must be a JSON object.");
            }

            var json = reader.ReadAsString();
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            if(!dict.ContainsKey("type"))
                throw new ConverterException("Chart object must contain a 'type' field.");

            // Deserializes the object based on its type.
            switch (dict["type"])
            {
                case ChartObjectType.TapNote:
                    return JsonConvert.DeserializeObject<TapNote>(json);
                case ChartObjectType.HoldNote:
                    return JsonConvert.DeserializeObject<HoldNote>(json);
                default:
                    throw new ConverterException($"Unknown chart object type: '{dict["type"]}'");
            }
        }

        public override void WriteJson(JsonWriter writer, IChartObject value, JsonSerializer serializer)
        {
            // Serializes the object based on its type.
            if (value is TapNote)
                writer.WriteRaw(JsonConvert.SerializeObject((TapNote)value));
            else if (value is HoldNote)
                writer.WriteRaw(JsonConvert.SerializeObject((HoldNote)value));
            else
                throw new ConverterException("Cannot serialize chart object, type is unknown.");
        }
    }
}