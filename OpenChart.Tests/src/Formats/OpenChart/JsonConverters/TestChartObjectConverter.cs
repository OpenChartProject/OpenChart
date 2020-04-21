using NUnit.Framework;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.Formats.OpenChart.Version0_1.JsonConverters;
using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System.Text.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestChartObjectConverter
    {
        JsonSerializerOptions options;

        [OneTimeSetUp]
        public void SetUp()
        {
            options = OpenChartSerializer.jsonOptions;
        }

        public void Test_Read_Null()
        {
            var data = JsonSerializer.Deserialize("null", typeof(IChartObject), options);
            Assert.IsNull(data);
        }

        [Test]
        public void Test_Read_TapNote()
        {
            var input = @"
                {
                    ""type"": ""tap""
                }
            ";
            var data = JsonSerializer.Deserialize(input, typeof(IChartObject), options);

            Assert.NotNull(data);
            Assert.IsInstanceOf(typeof(TapNote), data);
        }

        [Test]
        public void Test_Read_HoldNote()
        {
            var input = @"
                {
                    ""type"": ""hold"",
                    ""beatDuration"": 10
                }
            ";
            var data = JsonSerializer.Deserialize(input, typeof(IChartObject), options);

            Assert.NotNull(data);
            Assert.IsInstanceOf(typeof(HoldNote), data);
            Assert.AreEqual(10, (data as HoldNote).BeatDuration.Value);
        }
    }
}