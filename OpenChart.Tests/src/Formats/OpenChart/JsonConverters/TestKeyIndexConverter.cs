using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1.JsonConverters;
using System;
using System.Text.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestKeyIndexConverter
    {
        private class DummyData
        {
            public KeyIndex KeyIndex { get; set; }
        }

        JsonSerializerOptions options;

        [OneTimeSetUp]
        public void SetUp()
        {
            options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new KeyIndexConverter());
        }

        [TestCase("\"123\"")]
        [TestCase("1.2")]
        [TestCase("false")]
        public void Test_Read_BadType(string value)
        {
            var input = $"{{ \"keyIndex\": {value} }}";
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(input, typeof(DummyData), options));
        }

        [TestCase(-1)]
        public void Test_Read_InvalidValue(int value)
        {
            var input = $"{{ \"keyIndex\": {value} }}";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => JsonSerializer.Deserialize(input, typeof(DummyData), options)
            );
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(4)]
        public void Test_Read_ValidValue(int value)
        {
            var input = $"{{ \"keyIndex\": {value} }}";
            var data = (DummyData)JsonSerializer.Deserialize(input, typeof(DummyData), options);
            Assert.AreEqual(value, data.KeyIndex.Value);
        }
    }
}