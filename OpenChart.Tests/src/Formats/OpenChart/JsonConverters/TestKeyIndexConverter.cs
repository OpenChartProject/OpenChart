using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using System;
using Newtonsoft.Json;

namespace OpenChart.Tests.Formats.OpenChart.JsonConverters
{
    public class TestKeyIndexConverter
    {
        private class DummyData
        {
            public KeyIndex KeyIndex { get; set; }
        }

        JsonSerializerSettings settings = new OpenChartSerializer().Settings;

        [TestCase("null")]
        [TestCase("1.2")]
        [TestCase("false")]
        public void Test_Read_BadType(string value)
        {
            var input = $"{{ \"keyIndex\": {value} }}";
            Assert.Throws<ConverterException>(() => JsonConvert.DeserializeObject<DummyData>(input, settings));
        }

        [TestCase(-1)]
        public void Test_Read_InvalidValue(int value)
        {
            var input = $"{{ \"keyIndex\": {value} }}";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => JsonConvert.DeserializeObject<DummyData>(input, settings)
            );
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(4)]
        public void Test_Read_ValidValue(int value)
        {
            var input = $"{{ \"keyIndex\": {value} }}";
            var data = (DummyData)JsonConvert.DeserializeObject<DummyData>(input, settings);
            Assert.AreEqual(value, data.KeyIndex.Value);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Test_Write(int value)
        {
            var data = new DummyData() { KeyIndex = value };
            var json = JsonConvert.SerializeObject(data, typeof(DummyData), settings);
            Assert.AreEqual($"{{\"keyIndex\":{value}}}", json);
        }
    }
}
