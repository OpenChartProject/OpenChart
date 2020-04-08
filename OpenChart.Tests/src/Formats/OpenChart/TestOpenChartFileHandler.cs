using NUnit.Framework;
using OpenChart.Formats.OpenChart;

namespace OpenChart.Tests.Formats.OpenChart
{
    public class TestOpenChartFileHandler
    {
        [Test]
        public void Test_Version()
        {
            Assert.AreEqual("0.1", OpenChartFileHandler.Version);
        }

        [Test]
        public void Test_FileExtension()
        {
            Assert.AreEqual(".oc", OpenChartFileHandler.FileExtension);
        }
    }
}