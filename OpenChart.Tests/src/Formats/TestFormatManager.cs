using NUnit.Framework;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;

namespace OpenChart.Tests.Formats
{
    public class TestFormatManager
    {
        [Test]
        public void Test_GetFormatHandler()
        {
            var handler = new OpenChartFormatHandler();
            var manager = new FormatManager();

            Assert.Null(manager.GetFormatHandler(handler.FileExtension));
            manager.AddFormat(handler);
            Assert.AreSame(handler, manager.GetFormatHandler(handler.FileExtension));
        }
    }
}