using NUnit.Framework;
using OpenChart.Projects;
using OpenChart.UI.Actions;

namespace OpenChart.Tests.Actions
{
    public class TestCloseProjectAction
    {
        DummyApp app;
        CloseProjectAction action;

        [SetUp]
        public void SetUp()
        {
            app = new DummyApp();
            action = new CloseProjectAction(app);
        }

        [Test]
        public void Test_Init()
        {
            Assert.False(action.Action.Enabled);
        }

        [Test]
        public void Test_Enabled_ChangesWhenProjectChanges()
        {
            app.GetData().CurrentProject = new Project();
            Assert.True(action.Action.Enabled);

            app.GetData().CurrentProject = null;
            Assert.False(action.Action.Enabled);
        }

        [Test]
        public void Test_OnActivate_ClosesCurrentProject()
        {
            app.GetData().CurrentProject = new Project();
            action.Action.Activate(null);
            Assert.Null(app.GetData().CurrentProject);
        }
    }
}
