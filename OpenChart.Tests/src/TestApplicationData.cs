using NUnit.Framework;
using OpenChart.Projects;

namespace OpenChart.Tests
{
    public class TestApplicationData
    {
        [Test]
        public void Test_Init()
        {
            var appData = new ApplicationData("test");

            Assert.AreEqual("test", appData.AppFolder);
            Assert.IsNull(appData.CurrentProject);
            Assert.NotNull(appData.Formats);
            Assert.NotNull(appData.NoteSkins);
            Assert.NotNull(appData.NoteSkinFolder);
        }

        [Test]
        public void Test_ProjectChanged_EventArgs()
        {
            var appData = new ApplicationData("");
            var project = new Project();
            ProjectChangedEventArgs lastArgs = null;

            appData.ProjectChanged += (o, e) =>
            {
                lastArgs = e;
            };

            appData.CurrentProject = project;
            Assert.IsNull(lastArgs.OldProject);
            Assert.AreSame(project, lastArgs.NewProject);

            appData.CurrentProject = null;
            Assert.AreSame(project, lastArgs.OldProject);
            Assert.IsNull(lastArgs.NewProject);
        }

        [Test]
        public void Test_ProjectChanged_FiresOnlyWhenChanged()
        {
            var appData = new ApplicationData("");
            var calls = 0;

            appData.ProjectChanged += delegate { calls++; };
            appData.CurrentProject = null;

            Assert.AreEqual(0, calls);
            appData.CurrentProject = new Project();
            Assert.AreEqual(1, calls);
            appData.CurrentProject = null;
            Assert.AreEqual(2, calls);
        }
    }
}
