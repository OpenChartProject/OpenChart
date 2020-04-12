using OpenChart.Charting;
using NUnit.Framework;
using System;

namespace OpenChart.Tests.Charting
{
    public class TestChart
    {
        [Test]
        public void Test_Init()
        {
            var chart = new Chart(7);

            Assert.AreEqual(7, chart.KeyCount);
            Assert.AreEqual(0, chart.BPMs.Length);
            Assert.AreEqual(chart.KeyCount, chart.Objects.Length);

            for (var i = 0; i < chart.KeyCount; i++)
            {
                Assert.AreEqual(0, chart.Objects[i].Count);
            }
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Test_KeyCountLessThanOne(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Chart(value));
        }

        [Test]
        public void Test_BPMs()
        {
            var chart = new Chart(1);
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
            };

            chart.AddBPM(bpms[0]);
            chart.AddBPM(bpms[1]);

            Assert.AreEqual(2, chart.BPMs.Length);
            Assert.AreEqual(bpms[0], chart.BPMs[0]);
            Assert.AreEqual(bpms[1], chart.BPMs[1]);
        }

        [Test]
        public void Test_BPMs_IsCached()
        {
            var chart = new Chart(1);
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
            };

            chart.AddBPM(bpms[0]);
            chart.AddBPM(bpms[1]);

            Assert.AreSame(chart.BPMs, chart.BPMs);
        }

        [Test]
        public void Test_AddBPM_FirstBPMNonZeroBeat()
        {
            var chart = new Chart(1);
            var bpm = new BPM(100, 1);

            Assert.Throws<ArgumentException>(() => chart.AddBPM(bpm));
        }

        [Test]
        public void Test_AddBPM_FirstBPMZeroBeat()
        {
            var chart = new Chart(1);
            var bpm = new BPM(100, 0);

            Assert.DoesNotThrow(() => chart.AddBPM(bpm));
            Assert.AreEqual(1, chart.BPMs.Length);
            Assert.AreEqual(bpm, chart.BPMs[0]);
        }
    }
}