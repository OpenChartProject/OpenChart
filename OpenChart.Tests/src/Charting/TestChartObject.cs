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
        [TestCase(-999)]
        [TestCase(-1)]
        public void Test_KeyLessThanZero(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DummyObject(value));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Test_KeyGteZero(int value)
        {
            Assert.DoesNotThrow(() => new DummyObject(value));
        }
    }
}