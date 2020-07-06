using NUnit.Framework;
using OpenChart.Formats.StepMania.SM.Data;

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
            var chart = new Chart { ChartType = chartType };
            Assert.AreEqual(expected, chart.GetKeyCount());
        }
    }
}
