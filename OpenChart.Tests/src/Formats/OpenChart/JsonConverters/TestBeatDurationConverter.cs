using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1;
using System;
using Newtonsoft.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestBeatDurationConverter
    {
        private class DummyData
        {
            public BeatDuration Duration { get; set; }
        }

        JsonSerializerSettings settings = new OpenChartSerializer().Settings;


        [TestCase("\"123\"")]
        [TestCase("false")]
        public void Test_Read_BadType(string value)
        {
            var input = $"{{ \"duration\": {value} }}";
            Assert.Throws<JsonException>(() => JsonConvert.DeserializeObject<DummyData>(input, settings));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Test_Read_InvalidValue(double value)
        {
            var input = $"{{ \"duration\": {value} }}";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => JsonConvert.DeserializeObject<DummyData>(input, settings)
            );
        }

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(123.45)]
        public void Test_Read_ValidValue(double value)
        {
            var input = $"{{ \"duration\": {value} }}";
            var data = (DummyData)JsonConvert.DeserializeObject<DummyData>(input, settings);
            Assert.AreEqual(value, data.Duration.Value);
        }

        [TestCase(1)]
        [TestCase(1.5)]
        [TestCase(123.45)]
        public void Test_Write(double value)
        {
            var data = new DummyData() { Duration = value };
            var json = JsonConvert.SerializeObject(data, typeof(DummyData), settings);
            Assert.AreEqual($"{{\"duration\":{value}}}", json);
        }
    }
}
