using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Formats.OpenChart.Version0_1;
using System.IO;

namespace OpenChart.Tests.Formats.OpenChart
{
    public class TestOpenChartFormatHandler
    {
        OpenChartFormatHandler handler;

        [SetUp]
        public void SetUp()
        {
            handler = new OpenChartFormatHandler();
        }

        [Test]
        public void Test_FileExtension()
        {
            Assert.AreEqual(".oc", handler.FileExtension);
        }
    }
}