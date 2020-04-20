using NUnit.Framework;
using System;

namespace OpenChart.Tests.Charting.Objects
{
    public class TestChartObject
    {
        [TestCase(-999)]
        [TestCase(-1)]
        public void Test_KeyLessThanZero(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DummyObject(value, 0));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Test_KeyGteZero(int value)
        {
            Assert.DoesNotThrow(() => new DummyObject(value, 0));
        }
    }
}