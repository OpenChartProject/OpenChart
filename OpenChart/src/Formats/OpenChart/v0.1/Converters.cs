using OpenChart.Charting;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// JSON converter for a Beat object.
    /// </summary>
    public class BeatConverter : JsonConverter<Beat>
    {
        public override Beat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Beat(reader.GetDouble());
        }

        public override void Write(Utf8JsonWriter writer, Beat value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }

    /// <summary>
    /// JSON converter for a BeatDuration object.
    /// </summary>
    public class BeatDurationConverter : JsonConverter<BeatDuration>
    {
        public override BeatDuration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new BeatDuration(reader.GetDouble());
        }

        public override void Write(Utf8JsonWriter writer, BeatDuration value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }

    /// <summary>
    /// JSON converter for a Key object.
    /// </summary>
    public class KeyConverter : JsonConverter<Key>
    {
        public override Key Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Key(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Key value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }

    /// <summary>
    /// JSON converter for a KeyCount object.
    /// </summary>
    public class KeyCountConverter : JsonConverter<KeyCount>
    {
        public override KeyCount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new KeyCount(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, KeyCount value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }
}