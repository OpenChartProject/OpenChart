using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Tests.Charting
{
    public class TestChartEventBus
    {
        [Test]
        public void Test_Init()
        {
            var chart = new Chart(4);
            var events = new ChartEventBus(chart);

            Assert.AreSame(chart, events.Chart);
        }

        [Test]
        public void Test_Init_CannotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartEventBus(null));
        }

        [Test]
        public void Test_BPMAdded()
        {
            var chart = new Chart(4);
            var events = new ChartEventBus(chart);
            var bpm = new BPM(100, 0);
            var calls = 0;

            events.BPMAdded += (o, e) =>
            {
                Assert.AreSame(bpm, e.BPM);

                calls++;
            };

            chart.BPMList.BPMs.Add(bpm);
            Assert.AreEqual(1, calls);
        }

        [Test]
        public void Test_BPMChanged()
        {
            var chart = new Chart(4);
            var events = new ChartEventBus(chart);
            var bpm = new BPM(100, 0);
            var calls = 0;

            events.BPMChanged += (o, e) =>
            {
                Assert.AreSame(bpm, e.BPM);

                calls++;
            };

            chart.BPMList.BPMs.Add(bpm);

            bpm.Value = 150;

            Assert.AreEqual(1, calls);
        }

        [Test]
        public void Test_BPMRemoved()
        {
            var chart = new Chart(4);
            var events = new ChartEventBus(chart);
            var bpm = new BPM(100, 0);
            var calls = 0;

            events.BPMRemoved += (o, e) =>
            {
                Assert.AreSame(bpm, e.BPM);

                calls++;
            };

            chart.BPMList.BPMs.Add(bpm);
            chart.BPMList.BPMs.Remove(bpm);

            Assert.AreEqual(1, calls);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_ObjectAdded(int keyIndex)
        {
            var chart = new Chart(4);
            var events = new ChartEventBus(chart);
            var obj = new TapNote(keyIndex, 0);
            var calls = 0;

            events.ObjectAdded += (o, e) =>
            {
                Assert.AreSame(obj, e.Object);
                Assert.AreEqual(obj.KeyIndex, e.Object.KeyIndex);

                calls++;
            };

            chart.Objects[keyIndex].Add(obj);
            Assert.AreEqual(1, calls);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_ObjectRemoved(int keyIndex)
        {
            var chart = new Chart(4);
            var events = new ChartEventBus(chart);
            var obj = new TapNote(keyIndex, 0);
            var calls = 0;

            events.ObjectRemoved += (o, e) =>
            {
                Assert.AreSame(obj, e.Object);

                calls++;
            };

            chart.Objects[keyIndex].Add(obj);
            chart.Objects[keyIndex].Remove(obj);
            Assert.AreEqual(1, calls);
        }
    }
}
