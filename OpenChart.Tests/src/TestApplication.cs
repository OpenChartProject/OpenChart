using NUnit.Framework;
using OpenChart.UI.MenuActions;

namespace OpenChart.Tests
{
    public class TestApplication
    {
        Application app;

        [SetUp]
        public void SetUp()
        {
            app = new Application();
        }

        [Test]
        public void Test_Init()
        {
            Assert.AreEqual(Application.AppId, app.ApplicationId);
            Assert.NotNull(app.ActionDict);
            Assert.False(string.IsNullOrWhiteSpace(app.LogFile));
        }

        [Test]
        public void Test_InitApplication_OK()
        {
            Assert.True(app.InitApplication(skipAudio: true));
        }

        [Test]
        public void Test_InitApplication_AddsActions()
        {
            app.InitApplication(skipAudio: true);

            Assert.True(app.ActionDict.ContainsKey(NewProjectAction.Name));
            Assert.True(app.ActionDict.ContainsKey(CloseProjectAction.Name));
            Assert.True(app.ActionDict.ContainsKey(SaveAction.Name));
            Assert.True(app.ActionDict.ContainsKey(SaveAsAction.Name));
            Assert.True(app.ActionDict.ContainsKey(QuitAction.Name));
        }
    }
}
