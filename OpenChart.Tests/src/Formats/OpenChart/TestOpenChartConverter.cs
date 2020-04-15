using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Formats.OpenChart.Version0_1;

namespace OpenChart.Tests.Formats.OpenChart
{
    public class TestOpenChartConverter
    {
        OpenChartConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new OpenChartConverter();
        }

        [Test]
        public void Test_ToNative_Empty()
        {
            var fd = new FileData();
            fd.Metadata = new FileMetadata();
            fd.Metadata.KeyCount = 4;
            var chart = converter.ToNative(fd);

            Assert.AreEqual(0, chart.BPMs.Length);
            Assert.AreEqual(fd.Metadata.KeyCount, chart.KeyCount);
            Assert.AreEqual(fd.Metadata.KeyCount, chart.Objects.Length);
        }

        [Test]
        public void Test_FromNative_Empty()
        {
            var chart = new Chart(4);
            var fd = converter.FromNative(chart);

            Assert.AreEqual(chart.KeyCount, fd.Metadata.KeyCount);
            Assert.AreEqual("0.1", fd.Metadata.Version);
        }
    }
}
