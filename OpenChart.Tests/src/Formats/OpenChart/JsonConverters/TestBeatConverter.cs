using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1.JsonConverters;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestBeatConverter
    {
        private class DummyData
        {
            public Beat Beat { get; set; }
        }

        JsonSerializerSettings options;

        [OneTimeSetUp]
        public void SetUp()
        {
            options = new JsonSerializerSettings();

            options.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            options.Converters.Add(new BeatConverter());
            options.Converters.Add(new BeatDurationConverter());
            options.Converters.Add(new KeyIndexConverter());
            options.Converters.Add(new KeyCountConverter());
            options.Converters.Add(new ChartObjectConverter());
        }

        [TestCase("\"123\"")]
        [TestCase("false")]
        public void Test_Read_BadType(string value)
        {
            var input = $"{{ \"beat\": {value} }}";
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(input, typeof(DummyData), options));
        }

        [TestCase(-1)]
        public void Test_Read_InvalidValue(double value)
        {
            var input = $"{{ \"beat\": {value} }}";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => JsonSerializer.Deserialize(input, typeof(DummyData), options)
            );
        }

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(123.45)]
        public void Test_Read_ValidValue(double value)
        {
            var input = $"{{ \"beat\": {value} }}";
            var data = (DummyData)JsonSerializer.Deserialize(input, typeof(DummyData), options);
            Assert.AreEqual(value, data.Beat.Value);
        }

        [TestCase(0)]
        [TestCase(1.5)]
        [TestCase(123.45)]
        public void Test_Write(double value)
        {
            var data = new DummyData() { Beat = value };
            var json = JsonSerializer.Serialize(data, typeof(DummyData), options);
            Assert.AreEqual($"{{\"beat\":{value}}}", json);
        }
    }
}