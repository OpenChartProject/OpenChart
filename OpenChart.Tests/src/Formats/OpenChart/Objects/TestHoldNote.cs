using NUnit.Framework;
using OpenChart.Formats.OpenChart.Version0_1.Objects;

namespace OpenChart.Tests.Formats.OpenChart.Objects
{
    public class TestHoldNote
    {
        [Test]
        public void Test_Type()
        {
            Assert.AreEqual(ChartObjectType.HoldNote, new HoldNote().Type);
        }

        [Test]
        public void Test_Equals()
        {
            Assert.AreEqual(new HoldNote { Length = 1 }, new HoldNote { Length = 1 });
            Assert.AreNotEqual(new HoldNote(), null);
            Assert.AreNotEqual(new HoldNote { Length = 2 }, new HoldNote { Length = 1 });
            Assert.AreNotEqual(new DummyObject(), new HoldNote());
        }
    }
}
