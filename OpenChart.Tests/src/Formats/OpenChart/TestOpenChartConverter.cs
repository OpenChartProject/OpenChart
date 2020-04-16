using NUnit.Framework;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Projects;

namespace OpenChart.Tests.Formats.OpenChart
{
    public class TestOpenChartConverter
    {
        OpenChartConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new OpenChartConverter();
        }

        [Test]
        public void Test_SupportsMultipleExports()
        {
            Assert.IsTrue(OpenChartConverter.SupportsMultipleExports);
        }

        [Test]
        public void Test_ToNative_Empty()
        {
            var data = new ProjectData();
            data.Metadata.Version = OpenChartFormatHandler.Version;

            var project = converter.ToNative(data);

            Assert.IsEmpty(project.Charts);
            Assert.IsNull(project.SongMetadata);
        }

        [Test]
        public void Test_FromNative_Empty()
        {
            var project = new Project();
            var data = converter.FromNative(project);

            Assert.AreEqual(OpenChartFormatHandler.Version, data.Metadata.Version);
            Assert.IsEmpty(data.Charts);
            Assert.IsNull(data.Song);
        }
    }
}