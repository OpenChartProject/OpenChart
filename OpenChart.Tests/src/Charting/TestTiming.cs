using NUnit.Framework;
using OpenChart.Charting;
using System;

namespace OpenChart.Tests.Charting
{
    class TimingDummy : Timing
    {
        public TimingDummy(double beat) : base(beat) { }
    }

    public class TestTiming
    {
        [TestCase(-1)]
        [TestCase(-0.001)]
        public void Test_NegativeBeat(double value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TimingDummy(value));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Test_NonNegativeBeat(double value)
        {
            Assert.DoesNotThrow(() => new TimingDummy(value));
        }
    }
}
