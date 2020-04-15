using NUnit.Framework;
using OpenChart.NoteSkins;
using System;

namespace OpenChart.Tests.UI.Widgets
{
    public class TestNoteFieldWidget
    {
        [TestCase(-999)]
        [TestCase(-1)]
        public void Test_Key_CannotBeNegative(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DummyNoteFieldWidget(null, value));
        }

        [Test]
        public void Test_NoteSkin_CanBeNull()
        {
            Assert.DoesNotThrow(() => new DummyNoteFieldWidget(null, 0));
        }

        [Test]
        public void Test_OnKeyChanged_Fires()
        {
            var widget = new DummyNoteFieldWidget(null, 0);
            var calls = 0;

            widget.KeyChanged += delegate { calls++; };

            // No change
            widget.Key = 0;
            Assert.AreEqual(0, calls);

            widget.Key = 1;
            Assert.AreEqual(1, calls);

            widget.Key = 0;
            Assert.AreEqual(2, calls);
        }

        [Test]
        public void Test_OnNoteSkinChanged_Fires()
        {
            var fakeSkin = new KeyModeSkin(4);
            var widget = new DummyNoteFieldWidget(fakeSkin, 0);
            var calls = 0;

            widget.NoteSkinChanged += delegate { calls++; };

            // No change
            widget.NoteSkin = fakeSkin;
            Assert.AreEqual(0, calls);

            widget.NoteSkin = null;
            Assert.AreEqual(1, calls);

            widget.NoteSkin = fakeSkin;
            Assert.AreEqual(2, calls);
        }
    }
}
