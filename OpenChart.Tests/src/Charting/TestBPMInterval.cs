using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Tests.Charting
{
    public class TestBPMInterval
    {
        [Test]
        public void Test_Init()
        {
            var bpm = new BPM(120, 0);
            var interval = new BPMInterval(bpm, 1);

            Assert.AreSame(bpm, interval.BPM);
            Assert.AreEqual(1, interval.Seconds);
        }

        [TestCase(0)]
        [TestCase(0.999)]
        public void Test_BeatToTime_BeatOutOfRange(double beat)
        {
            var bpm = new BPM(120, 1);
            var interval = new BPMInterval(bpm, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => interval.BeatToTime(beat));
        }

        [TestCase(0, 0)]
        [TestCase(1, 0.5)]
        [TestCase(10, 5)]
        public void Test_BeatToTime_ReturnsExpected(double beat, double expected)
        {
            var bpm = new BPM(120, 0);
            var interval = new BPMInterval(bpm, 0);

            Assert.AreEqual(expected, interval.BeatToTime(beat));
        }

        [TestCase(0)]
        [TestCase(0.999)]
        public void Test_TimeToBeat_TimeOutOfRange(double time)
        {
            var bpm = new BPM(120, 1);
            var interval = new BPMInterval(bpm, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => interval.TimeToBeat(time));
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(10, 20)]
        public void Test_TimeToBeat_ReturnsExpected(double time, double expected)
        {
            var bpm = new BPM(120, 0);
            var interval = new BPMInterval(bpm, 0);

            Assert.AreEqual(expected, interval.TimeToBeat(time).Value);
        }
    }
}
