using NUnit.Framework;
using OpenChart.Projects;

namespace OpenChart.Tests
{
    public class TestApplicationEventBus
    {
        ApplicationData AppData;
        ApplicationEventBus EventBus;

        [SetUp]
        public void SetUp()
        {
            AppData = new ApplicationData("");
            EventBus = new ApplicationEventBus(AppData);
        }

        [Test]
        public void Test_CurrentProjectChanged_Fires()
        {
            var calls = 0;

            EventBus.CurrentProjectChanged += delegate { calls++; };

            AppData.CurrentProject = null;
            Assert.AreEqual(0, calls);

            AppData.CurrentProject = new Project();
            Assert.AreEqual(1, calls);
        }

        [Test]
        public void Test_CurrentProjectRenamed_FiresWhenChanged()
        {
            var calls = 0;
            var project = new Project();

            EventBus.CurrentProjectRenamed += delegate { calls++; };

            AppData.CurrentProject = project;
            Assert.AreEqual(0, calls);

            AppData.CurrentProject.Name = "qwerty";
            Assert.AreEqual(1, calls);

            AppData.CurrentProject.Name = AppData.CurrentProject.Name;
            Assert.AreEqual(1, calls);
        }

        [Test]
        public void Test_CurrentProjectRenamed_RemovesListenerWhenProjectChanges()
        {
            var calls = 0;
            var project = new Project();

            EventBus.CurrentProjectRenamed += delegate { calls++; };

            AppData.CurrentProject = project;
            AppData.CurrentProject = null;

            project.Name = "qwerty";
            Assert.AreEqual(0, calls);
        }
    }
}
