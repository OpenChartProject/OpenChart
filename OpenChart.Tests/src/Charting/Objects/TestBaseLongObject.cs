using NUnit.Framework;
using OpenChart.Charting.Exceptions;
using System;

namespace OpenChart.Tests.Charting.Objects
{
    public class TestBaseLongObject
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Test_Length_MustBeGreaterThanZero(double value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DummyLongObject(0, 0, value));
        }

        [TestCase(0.001)]
        [TestCase(1)]
        public void Test_Length_GreaterThanZero(double value)
        {
            Assert.DoesNotThrow(() => new DummyLongObject(0, 0, value));
        }

        [Test]
        public void Test_CanBeInserted_OK()
        {
            var hold = new DummyLongObject(0, 10, 5);

            // No issues inserting.
            Assert.DoesNotThrow(() => hold.ValidOrThrow(null, null));
            Assert.DoesNotThrow(() => hold.ValidOrThrow(new DummyObject(0, 0), null));
            Assert.DoesNotThrow(() => hold.ValidOrThrow(null, new DummyObject(0, 20)));
            Assert.DoesNotThrow(() => hold.ValidOrThrow(new DummyObject(0, 0), new DummyObject(0, 20)));
        }

        [Test]
        public void Test_CanBeInserted_Obstruction()
        {
            var hold = new DummyLongObject(0, 10, 5);

            Assert.Throws<ObjectOverlapException>(
                () => hold.ValidOrThrow(new DummyObject(0, 0), new DummyObject(0, hold.Beat.Value + hold.Length.Value))
            );
        }
    }
}