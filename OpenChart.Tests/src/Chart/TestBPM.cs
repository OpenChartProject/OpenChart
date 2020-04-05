using NUnit.Framework;
using OpenChart.Chart;
using System;

namespace OpenChart.Tests.Chart
{
    public class TestBPM
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Test_NonPositiveBPM(double value)
        {
            Assert.Throws<ArgumentException>(() => new BPM(value, 0));
        }

        [TestCase(0.0001)]
        [TestCase(100)]
        public void Test_PositiveBPM(double value)
        {
            Assert.DoesNotThrow(() => new BPM(value, 0));
        }
    }
}