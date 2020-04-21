using NUnit.Framework;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1.JsonConverters;
using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System;
using System.Text.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestChartObjectConverter
    {
        JsonSerializerOptions options;

        [OneTimeSetUp]
        public void SetUp()
        {
            options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new ChartObjectConverter());
        }

        public void Test_Read_Null()
        {
            var data = JsonSerializer.Deserialize("null", typeof(IChartObject), options);
            Assert.IsNull(data);
        }

        [TestCase("tap", typeof(TapNote))]
        public void Test_Read_ValidSimpleObjects(string value, Type expectedType)
        {
            var input = $"\"{value}\"";
            var data = JsonSerializer.Deserialize(input, typeof(IChartObject), options);
            Assert.NotNull(data);
            Assert.IsInstanceOf(expectedType, data);
        }

        [TestCase("hold")]
        [TestCase("invalid")]
        public void Test_Read_InvalidSimpleObjectsThrows(string value)
        {
            var input = $"\"{value}\"";
            Assert.Throws<ConverterException>(() => JsonSerializer.Deserialize(input, typeof(IChartObject), options));
        }

        [Test]
        public void Test_Read_HoldNote()
        {
            var input = @"
                [
                    ""hold"",
                    {
                        ""beatDuration"": 10
                    }
                ]
            ";
            var data = JsonSerializer.Deserialize(input, typeof(IChartObject), options);

            Assert.NotNull(data);
            Assert.IsInstanceOf(typeof(HoldNote), data);
            Assert.AreEqual(10, (data as HoldNote).BeatDuration.Value);
        }
    }
}