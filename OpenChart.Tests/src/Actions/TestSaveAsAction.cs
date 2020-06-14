using NUnit.Framework;
using OpenChart.Projects;
using OpenChart.UI.MenuActions;

namespace OpenChart.Tests.Actions
{
    public class TestSaveAsAction
    {
        DummyApp app;
        SaveAsAction action;

        [SetUp]
        public void SetUp()
        {
            app = new DummyApp();
            action = new SaveAsAction(app);
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
    }
}
