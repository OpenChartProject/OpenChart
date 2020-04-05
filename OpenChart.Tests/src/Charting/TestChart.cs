using OpenChart.Charting;
using NUnit.Framework;
using System;

namespace OpenChart.Tests.Charting
{
    public class TestChart
    {
        [TestCase]
        public void Test_AddBPMChange_FirstBPMNonZero()
        {
            var chart = new Chart();
            var bpm = new BPM(100, 1);

            Assert.Throws<ArgumentException>(() => chart.AddBPMChange(bpm));
        }
    }
}