using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                throw new ConverterException("Chart object must be a JSON object.");

            var jObj = JObject.Load(reader);

            if (!jObj.ContainsKey("type"))
                throw new ConverterException("Chart object must have a 'type' key.");

            var type = (string)jObj["type"];
            IChartObject result;

            // Look at the type key to see what kind of chart object this is.
            switch (type)
            {
                case ChartObjectType.TapNote:
                    result = new TapNote();
                    break;
                case ChartObjectType.HoldNote:
                    result = new HoldNote();
                    break;
                default:
                    throw new ConverterException($"Unknown chart object type: '{type}'");
            }

            serializer.Populate(jObj.CreateReader(), result);

            return result;
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
