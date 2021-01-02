using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using System;
using Newtonsoft.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestKeyCountConverter
    {
        private class DummyData
        {
            public KeyCount KeyCount { get; set; }
        }

        JsonSerializerSettings settings = new OpenChartSerializer().Settings;


        [TestCase("null")]
        [TestCase("1.2")]
        [TestCase("false")]
        public void Test_Read_BadType(string value)
        {
            var input = $"{{ \"keyCount\": {value} }}";
            Assert.Throws<ConverterException>(() => JsonConvert.DeserializeObject<DummyData>(input, settings));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Test_Read_InvalidKeyCount(int value)
        {
            var input = $"{{ \"keyCount\": {value} }}";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => JsonConvert.DeserializeObject<DummyData>(input, settings)
            );
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(7)]
        public void Test_Read_ValidKeyCount(int value)
        {
            var input = $"{{ \"keyCount\": {value} }}";
            var data = (DummyData)JsonConvert.DeserializeObject<DummyData>(input, settings);
            Assert.AreEqual(value, data.KeyCount.Value);
        }

        [TestCase(1)]
        [TestCase(4)]
        public void Test_Write(int value)
        {
            var data = new DummyData() { KeyCount = value };
            var json = JsonConvert.SerializeObject(data, typeof(DummyData), settings);
            Assert.AreEqual($"{{\"keyCount\":{value}}}", json);
        }
    }
}
