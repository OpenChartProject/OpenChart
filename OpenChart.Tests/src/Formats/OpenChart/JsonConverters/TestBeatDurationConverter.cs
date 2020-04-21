using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1.JsonConverters;
using System;
using System.Text.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestBeatDurationConverter
    {
        private class DummyData
        {
            public BeatDuration Duration { get; set; }
        }

        JsonSerializerOptions options;

        [OneTimeSetUp]
        public void SetUp()
        {
            options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new BeatDurationConverter());
        }

        [TestCase("\"123\"")]
        [TestCase("false")]
        public void Test_Read_BadType(string value)
        {
            var input = $"{{ \"duration\": {value} }}";
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(input, typeof(DummyData), options));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Test_Read_InvalidValue(double value)
        {
            var input = $"{{ \"duration\": {value} }}";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => JsonSerializer.Deserialize(input, typeof(DummyData), options)
            );
        }

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(123.45)]
        public void Test_Read_ValidValue(double value)
        {
            var input = $"{{ \"duration\": {value} }}";
            var data = (DummyData)JsonSerializer.Deserialize(input, typeof(DummyData), options);
            Assert.AreEqual(value, data.Duration.Value);
        }
    }
}