using NUnit.Framework;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Tests.Charting.Properties
{
    public class TestKey
    {
        [TestCase(-999)]
        [TestCase(-1)]
        public void Test_NegativeKey(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new KeyIndex(value));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Test_NonNegativeKey(int value)
        {
            Assert.DoesNotThrow(() => new KeyIndex(value));
        }

        [Test]
        public void Test_OnKeyChanged()
        {
            var key = new KeyIndex(0);
            var calls = 0;

            key.Changed += delegate { calls++; };

            key.Value = key.Value;
            Assert.AreEqual(0, calls);

            key.Value++;
            Assert.AreEqual(1, calls);
        }
    }
}
