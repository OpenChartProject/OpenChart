using GLib;
using NUnit.Framework;
using OpenChart.Tests;
using OpenChart.UI.Assets;
using System.IO;

namespace OpenChart.Tests.UI.Assets
{
    public class TestImageAsset
    {
        Config config = Config.Get();

        [Test]
        public void Test_BadFilePath()
        {
            Assert.Throws<FileNotFoundException>(
                () => new ImageAsset(Path.Join(config.TestDataPath, "fake"))
            );
        }

        [Test]
        public void Test_LoadsImageFile()
        {
            var path = Path.Join(config.TestDataPath, "image.png");
            var image = new ImageAsset(path);

            Assert.AreEqual(path, image.Path);
            Assert.NotNull(image.Data);
            Assert.NotNull(image.Pixbuf);
        }

        [Test]
        public void Test_FailsWhenNotImage()
        {
            var path = Path.Join(config.TestDataPath, "empty.txt");

            Assert.Throws<GException>(() => new ImageAsset(path));
        }
    }
}
