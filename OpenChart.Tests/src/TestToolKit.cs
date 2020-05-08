using NUnit.Framework;

namespace OpenChart.Tests
{
    public class TestToolKit
    {
        [Test]
        public void Test_GetInstance_ReturnsSameObject()
        {
            Assert.AreSame(ToolKit.GetInstance(), ToolKit.GetInstance());
        }

        [Test]
        public void Test_Has4kSkin()
        {
            Assert.NotNull(ToolKit.GetInstance().NoteSkin.GetKeyModeSkin(4));
        }
    }
}
