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
        public void Test_ValidatePlacement_DoesNotThrowWhenValid()
        {
            var hold = new DummyLongObject(0, 10, 5);

            // No issues inserting.
            Assert.DoesNotThrow(() => hold.ValidatePlacement(null, null));
            Assert.DoesNotThrow(() => hold.ValidatePlacement(new DummyObject(0, 0), null));
            Assert.DoesNotThrow(() => hold.ValidatePlacement(null, new DummyObject(0, 20)));
            Assert.DoesNotThrow(() => hold.ValidatePlacement(new DummyObject(0, 0), new DummyObject(0, 20)));
        }

        [Test]
        public void Test_ValidatePlacement_ThrowsWhenObstructed()
        {
            var hold = new DummyLongObject(0, 10, 5);

            Assert.Throws<ObjectOverlapException>(
                () => hold.ValidatePlacement(new DummyObject(0, 0), new DummyObject(0, hold.Beat.Value + hold.Length.Value))
            );
        }
    }
}
