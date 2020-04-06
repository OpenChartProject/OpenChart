using NUnit.Framework;
using OpenChart.Charting;
using System;

namespace OpenChart.Tests.Charting
{
    class DummyObject : ChartObject
    {
        public DummyObject(int keyCount) : base(keyCount, 0) { }
    }

    public class TestChartObject
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Test_KeyLessThanOne(int value)
        {
            Assert.Throws<ArgumentException>(() => new DummyObject(value));
        }

        [TestCase(1)]
        [TestCase(4)]
        public void Test_KeyGteOne(int value)
        {
            Assert.DoesNotThrow(() => new DummyObject(value));
        }
    }
}