using NUnit.Framework;
using OpenChart.NoteSkins;
using OpenChart.UI.Widgets;
using OpenChart.Tests;

namespace OpenChart.Tests.UI.Widgets
{
    public class TestTapNoteWidget
    {
        Config config = Config.Get();
        KeyModeSkin noteSkin;
        const int KeyCount = 4;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            noteSkin = config.NoteSkins.GetNoteSkin("default_arrow").GetKeyModeSkin(KeyCount);
        }

        [Test]
        public void Test_UsesCorrectImageAsset()
        {
            var widget = new TapNoteWidget(noteSkin, 0);

            for (var i = 0; i < KeyCount; i++)
            {
                widget.Key = i;
                Assert.AreSame(noteSkin.Keys[i].TapNote, widget.Image);
            }
        }

        [Test]
        public void Test_OnKeyChanged_UpdatesAppearance()
        {
            var widget = new TapNoteWidget(noteSkin, 0);
            var oldImage = widget.Image;

            widget.Key = 1;
            Assert.AreNotSame(oldImage, widget.Image);

            widget.Key = 0;
            Assert.AreSame(oldImage, widget.Image);
        }
    }
}
