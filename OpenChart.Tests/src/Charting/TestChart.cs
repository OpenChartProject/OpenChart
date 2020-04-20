using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Objects;
using System;
using System.Collections.Generic;

namespace OpenChart.Tests.Charting
{
    public class TestChart
    {
        Chart chart;

        [SetUp]
        public void SetUp()
        {
            chart = new Chart(4);
        }

        [Test]
        public void Test_Init_4k()
        {
            Assert.AreEqual(4, chart.KeyCount.Value);
            Assert.AreEqual(0, chart.BPMs.Count);
            Assert.AreEqual(chart.KeyCount.Value, chart.Objects.Length);

            for (var i = 0; i < chart.KeyCount.Value; i++)
            {
                Assert.AreEqual(0, chart.Objects[i].Count);
            }
        }
    }
}