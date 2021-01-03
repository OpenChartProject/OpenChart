using NUnit.Framework;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Tests.Charting.Properties
{
    public class TestBeatDivision
    {
        [TestCase(0)]
        [TestCase(-1)]
        public void Test_InvalidDivision(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BeatDivision(value));
        }

        [TestCase(0, 1, true)]
        [TestCase(0.00001, 1, true)]
        [TestCase(0.99999, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(1, 4, true)]
        [TestCase(0.25, 4, true)]
        [TestCase(0.5, 64, true)]
        [TestCase(30.1, 1, false)]
        public void Test_IsAligned(double beat, int div, bool aligned)
        {
            var bd = new BeatDivision(div);
            Assert.AreEqual(aligned, bd.IsAligned(beat));
        }

        [TestCase(0, 1, 1)]
        [TestCase(0.5, 1, 1)]
        [TestCase(0.99999, 2, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1.00001, 2, 1)]
        [TestCase(0.33, 0.5, 4)]
        [TestCase(1.99, 2, 8)]
        public void Test_NextDivisionFromBeat(double curBeat, double nextBeat, int div)
        {
            var bd = new BeatDivision(div);
            Assert.AreEqual(nextBeat, bd.NextDivisionFromBeat(curBeat).Value);
        }

        [TestCase(1, 0, 1)]
        [TestCase(0.5, 0, 1)]
        [TestCase(0.99999, 0, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1.00001, 0, 1)]
        [TestCase(0.5, 1.0 / 3, 3)]
        [TestCase(2, 1.875, 8)]
        public void Test_PrevDivisionFromBeat(double curBeat, double prevBeat, int div)
        {
            var bd = new BeatDivision(div);
            Assert.AreEqual(prevBeat, bd.PrevDivisionFromBeat(curBeat).Value);
        }
    }
}
