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
                var args = e as BPMEventArgs;

                Assert.AreSame(bpm, args.BPM);

                calls++;
            };

            chart.BPMs.Add(bpm);
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
                var args = e as BPMEventArgs;

                Assert.AreSame(bpm, args.BPM);

                calls++;
            };

            chart.BPMs.Add(bpm);

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
                var args = e as BPMEventArgs;

                Assert.AreSame(bpm, args.BPM);

                calls++;
            };

            chart.BPMs.Add(bpm);
            chart.BPMs.Remove(bpm);

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
                var args = e as ObjectEventArgs;

                Assert.AreSame(obj, args.Object);
                Assert.AreEqual(obj.KeyIndex, args.Object.KeyIndex);

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
                var args = e as ObjectEventArgs;

                Assert.AreSame(obj, args.Object);

                calls++;
            };

            chart.Objects[keyIndex].Add(obj);
            chart.Objects[keyIndex].Remove(obj);
            Assert.AreEqual(1, calls);
        }
    }
}
