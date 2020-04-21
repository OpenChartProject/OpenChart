using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1.JsonConverters;
using System;
using System.Text.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestKeyCountConverter
    {
        private class DummyData
        {
            public KeyCount KeyCount { get; set; }
        }

        JsonSerializerOptions options;

        [OneTimeSetUp]
        public void SetUp()
        {
            options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new KeyCountConverter());
        }

        [TestCase("\"123\"")]
        [TestCase("1.2")]
        [TestCase("false")]
        public void Test_Read_BadType(string value)
        {
            var input = $"{{ \"keyCount\": {value} }}";
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(input, typeof(DummyData), options));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Test_Read_InvalidKeyCount(int value)
        {
            var input = $"{{ \"keyCount\": {value} }}";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => JsonSerializer.Deserialize(input, typeof(DummyData), options)
            );
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(7)]
        public void Test_Read_ValidKeyCount(int value)
        {
            var input = $"{{ \"keyCount\": {value} }}";
            var data = (DummyData)JsonSerializer.Deserialize(input, typeof(DummyData), options);
            Assert.AreEqual(value, data.KeyCount.Value);
        }
    }
}