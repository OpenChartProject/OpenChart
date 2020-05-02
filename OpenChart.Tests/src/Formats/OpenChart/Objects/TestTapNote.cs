using NUnit.Framework;
using OpenChart.Formats.OpenChart.Version0_1.Objects;

namespace OpenChart.Tests.Formats.OpenChart.Objects
{
    public class TestTapNote
    {
        [Test]
        public void Test_Type()
        {
            Assert.AreEqual(ChartObjectType.TapNote, new TapNote().Type);
        }

        [Test]
        public void Test_Equals()
        {
            Assert.AreEqual(new TapNote(), new TapNote());
            Assert.AreNotEqual(new TapNote(), null);
            Assert.AreNotEqual(new TapNote(), new DummyObject());
        }
    }
}
