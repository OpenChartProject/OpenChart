using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Objects;
using System;

namespace OpenChart.Tests.Charting.Objects
{
    public class TestHoldNote
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Test_Length_MustBeGreaterThanZero(double value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HoldNote(0, 0, value));
        }

        [TestCase(0.001)]
        [TestCase(1)]
        public void Test_Length_GreaterThanZero(double value)
        {
            Assert.DoesNotThrow(() => new HoldNote(0, 0, value));
        }

        [Test]
        public void Test_CanBeInserted_OK()
        {
            var hold = new HoldNote(0, 10, 5);

            // No issues inserting.
            Assert.DoesNotThrow(() => hold.CanBeInserted(null, null));
            Assert.DoesNotThrow(() => hold.CanBeInserted(new TapNote(0, 0), null));
            Assert.DoesNotThrow(() => hold.CanBeInserted(null, new TapNote(0, 20)));
            Assert.DoesNotThrow(() => hold.CanBeInserted(new TapNote(0, 0), new TapNote(0, 20)));
        }

        [Test]
        public void Test_CanBeInserted_Obstruction()
        {
            var hold = new HoldNote(0, 10, 5);

            Assert.Throws<ChartException>(
                () => hold.CanBeInserted(new TapNote(0, 0), new TapNote(0, hold.Beat + hold.Length))
            );
        }
    }
}