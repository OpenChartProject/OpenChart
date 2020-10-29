using NUnit.Framework;
using OpenChart.UI.MenuActions;

namespace OpenChart.Tests.Actions
{
    public class TestNewProjectAction
    {
        DummyApp app;
        NewProjectAction action;

        [SetUp]
        public void SetUp()
        {
            app = new DummyApp();
            action = new NewProjectAction(app);
        }

        [Test]
        public void Test_Init()
        {
            Assert.True(action.Action.Enabled);
        }

        [Test]
        public void Test_OnActivate_CreatesNewProject()
        {
            Assert.IsNull(app.GetData().CurrentProject);
            action.Action.Activate(null);
            Assert.NotNull(app.GetData().CurrentProject);
        }
    }
}
