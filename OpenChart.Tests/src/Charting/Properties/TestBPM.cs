using NUnit.Framework;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Tests.Charting.Properties
{
    public class TestBPM
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Test_NonPositiveBPM(double value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BPM(value, 0));
        }

        [TestCase(0.0001)]
        [TestCase(100)]
        public void Test_PositiveBPM(double value)
        {
            Assert.DoesNotThrow(() => new BPM(value, 0));
        }

        [TestCase(100, 0)]
        [TestCase(101.5, 1.5)]
        public void Test_Equals_AreEqual(double bpmValue, double bpmBeat)
        {
            Assert.AreEqual(new BPM(100, 0), new BPM(100, 0));
        }

        [TestCase(100, 0)]
        [TestCase(101.5, 1.5)]
        public void Test_Equals_AreNotEqual(double bpmValue, double bpmBeat)
        {
            Assert.AreNotEqual(new BPM(bpmValue + 1, bpmBeat + 1), new BPM(bpmValue, bpmBeat));
        }
    }
}
