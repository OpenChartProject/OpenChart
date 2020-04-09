using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Formats.OpenChart;
using System.IO;

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

        [Test]
        public void Test_ConvertChartToFileData()
        {
            var chart = new Chart(4);
            var handler = new OpenChartFileHandler();
            var fd = handler.ConvertChartToFileData(chart);

            Assert.AreEqual(chart.KeyCount, fd.Metadata.KeyCount);
            Assert.AreEqual(OpenChartFileHandler.Version, fd.Metadata.Version);
        }

        [Test]
        public void Test_LoadFileData()
        {
            var oldChart = new Chart(4);
            var handler = new OpenChartFileHandler();
            var stream = new MemoryStream();

            handler.Write(oldChart, new StreamWriter(stream));
            stream.Position = 0;
            var newChart = handler.Read(new StreamReader(stream));

            Assert.AreEqual(oldChart, newChart);
        }
    }
}