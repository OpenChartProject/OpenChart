using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1;
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

        JsonSerializerSettings settings = new OpenChartSerializer().Settings;

        [TestCase("\"123\"")]
        [TestCase("false")]
        public void Test_Read_BadType(string value)
        {
            var input = $"{{ \"beat\": {value} }}";
            Assert.Throws<JsonException>(() => JsonConvert.DeserializeObject<DummyData>(input, settings));
        }

        [TestCase(-1)]
        public void Test_Read_InvalidValue(double value)
        {
            var input = $"{{ \"beat\": {value} }}";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => JsonConvert.DeserializeObject<DummyData>(input, settings)
            );
        }

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(123.45)]
        public void Test_Read_ValidValue(double value)
        {
            var input = $"{{ \"beat\": {value} }}";
            var data = (DummyData)JsonConvert.DeserializeObject<DummyData>(input, settings);
            Assert.AreEqual(value, data.Beat.Value);
        }

        [TestCase(0)]
        [TestCase(1.5)]
        [TestCase(123.45)]
        public void Test_Write(double value)
        {
            var data = new DummyData() { Beat = value };
            var json = JsonConvert.SerializeObject(data, typeof(DummyData), settings);
            Assert.AreEqual($"{{\"beat\":{value}}}", json);
        }
    }
}
