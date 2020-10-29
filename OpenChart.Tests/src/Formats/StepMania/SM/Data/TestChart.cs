using NUnit.Framework;
using OpenChart.Formats.StepMania.SM.Data;
using Enums = OpenChart.Formats.StepMania.SM.Enums;

namespace OpenChart.Tests.Formats.StepMania.SM.Data
{
    public class TestChart
    {
        [TestCase("dance-single", 4)]
        [TestCase("dance-solo", 6)]
        [TestCase("dance-double", 8)]
        [TestCase("foo", -1)]
        public void Test_GetKeyCount(string chartType, int expected)
        {
            var chart = new Chart();
            chart.ChartType = Enums.ChartType.Get(chartType);
            Assert.AreEqual(expected, chart.GetKeyCount());
        }
    }
}
