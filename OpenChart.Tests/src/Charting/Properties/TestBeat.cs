using NUnit.Framework;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Tests.Charting.Properties
{
    public class TestBeat
    {
        [TestCase(-1)]
        [TestCase(-0.001)]
        public void Test_NegativeBeat(double value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Beat(value));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Test_NonNegativeBeat(double value)
        {
            Assert.DoesNotThrow(() => new Beat(value));
        }

        [Test]
        public void Test_OnBeatChanged()
        {
            var beat = new Beat(0);
            var calls = 0;

            beat.Changed += delegate { calls++; };

            beat.Value = beat.Value;
            Assert.AreEqual(0, calls);

            beat.Value++;
            Assert.AreEqual(1, calls);
        }
    }
}
