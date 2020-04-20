using NUnit.Framework;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Tests.Charting.Properties
{
    public class TestBeatDuration
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Test_LTEZeroBeatDuration(double value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BeatDuration(value));
        }

        [TestCase(0.001)]
        [TestCase(1)]
        public void Test_GTZeroBeatDuration(double value)
        {
            Assert.DoesNotThrow(() => new BeatDuration(value));
        }

        [Test]
        public void Test_OnBeatDurationChanged()
        {
            var beatDuration = new BeatDuration(1);
            var calls = 0;

            beatDuration.Changed += delegate { calls++; };

            beatDuration.Value = beatDuration.Value;
            Assert.AreEqual(0, calls);

            beatDuration.Value++;
            Assert.AreEqual(1, calls);
        }
    }
}
