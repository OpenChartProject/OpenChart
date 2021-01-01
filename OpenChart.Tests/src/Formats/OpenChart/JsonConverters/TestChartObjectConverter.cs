using NUnit.Framework;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.Formats.OpenChart.Version0_1.Objects;
using Newtonsoft.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestChartObjectConverter
    {
        JsonSerializerSettings settings = new OpenChartSerializer().Settings;

        public void Test_Read_Null()
        {
            var data = JsonConvert.DeserializeObject<IChartObject>("null", settings);
            Assert.IsNull(data);
        }

        [TestCase("null")]
        [TestCase("false")]
        [TestCase("0")]
        [TestCase("[]")]
        public void Test_Read_BadJsonType(string input)
        {
            Assert.Throws<ConverterException>(() => JsonConvert.DeserializeObject<IChartObject>(input, settings));
        }

        [TestCase("")]
        [TestCase("TAP")]
        [TestCase("cock")]
        public void Test_Read_BadTypeString(string value)
        {
            var input = $"{{ \"type\": \"{value}\" }}";
            Assert.Throws<ConverterException>(() => JsonConvert.DeserializeObject<IChartObject>(input, settings));
        }

        [Test]
        public void Test_Read_TapNote()
        {
            var input = @"
                {
                    ""type"": ""tap""
                }
            ";
            var data = JsonConvert.DeserializeObject<IChartObject>(input, settings);

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
            var data = JsonConvert.DeserializeObject<IChartObject>(input, settings);

            Assert.NotNull(data);
            Assert.IsInstanceOf(typeof(HoldNote), data);
            Assert.AreEqual(10, (data as HoldNote).Length.Value);
        }

        [Test]
        public void Test_Write_TapNote()
        {
            var input = new TapNote();
            var data = JsonConvert.SerializeObject(input, settings);

            Assert.AreEqual("{\"type\":\"tap\"}", data);
        }

        [Test]
        public void Test_Write_HoldNote()
        {
            var input = new HoldNote() { Length = 2 };
            var data = JsonConvert.SerializeObject(input, settings);

            Assert.AreEqual("{\"length\":2.0,\"type\":\"hold\"}", data);
        }
    }
}
