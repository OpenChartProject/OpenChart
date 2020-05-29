using NUnit.Framework;
using OpenChart.UI.Actions;

namespace OpenChart.Tests.Actions
{
    public class TestQuitAction
    {
        DummyApp app;
        QuitAction action;

        [SetUp]
        public void SetUp()
        {
            app = new DummyApp();
            action = new QuitAction(app);
        }

        [Test]
        public void Test_Init()
        {
            Assert.True(action.Action.Enabled);
        }
    }
}
