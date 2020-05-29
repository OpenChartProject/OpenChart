using NUnit.Framework;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
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
            options = OpenChartSerializer.JsonOptions;
        }

        public void Test_Read_Null()
        {
            var data = JsonSerializer.Deserialize("null", typeof(IChartObject), options);
            Assert.IsNull(data);
        }

        [TestCase("false")]
        [TestCase("0")]
        [TestCase("[]")]
        public void Test_Read_BadJsonType(string input)
        {
            Assert.Throws<ConverterException>(() => JsonSerializer.Deserialize(input, typeof(IChartObject), options));
        }

        [TestCase("")]
        [TestCase("TAP")]
        [TestCase("cock")]
        public void Test_Read_BadTypeString(string value)
        {
            var input = $"{{ \"type\": \"{value}\" }}";
            Assert.Throws<ConverterException>(() => JsonSerializer.Deserialize(input, typeof(IChartObject), options));
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
                    ""length"": 10
                }
            ";
            var data = JsonSerializer.Deserialize(input, typeof(IChartObject), options);

            Assert.NotNull(data);
            Assert.IsInstanceOf(typeof(HoldNote), data);
            Assert.AreEqual(10, (data as HoldNote).Length.Value);
        }

        [Test]
        public void Test_Write_TapNote()
        {
            var input = new TapNote();
            var data = JsonSerializer.Serialize(input, typeof(IChartObject), options);

            Assert.AreEqual("{\"type\":\"tap\"}", data);
        }

        [Test]
        public void Test_Write_HoldNote()
        {
            var input = new HoldNote() { Length = 2 };
            var data = JsonSerializer.Serialize(input, typeof(IChartObject), options);

            Assert.AreEqual("{\"length\":2,\"type\":\"hold\"}", data);
        }
    }
}
