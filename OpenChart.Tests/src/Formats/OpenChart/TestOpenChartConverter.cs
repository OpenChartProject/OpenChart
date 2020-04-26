using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Projects;

namespace OpenChart.Tests.Formats.OpenChart
{
    public class TestOpenChartConverter
    {
        OpenChartConverter converter;

        private ProjectData newProjectData()
        {
            var data = new ProjectData();
            data.Metadata.Version = OpenChartFormatHandler.Version;
            return data;
        }

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
            var data = newProjectData();
            var native = converter.ToNative(data);

            Assert.IsEmpty(native.Charts);
            Assert.IsNull(native.SongMetadata);
        }

        [Test]
        public void Test_FromNative_Empty()
        {
            var native = new Project();
            var data = converter.FromNative(native);

            Assert.AreEqual(OpenChartFormatHandler.Version, data.Metadata.Version);
            Assert.IsEmpty(data.Charts);
            Assert.IsNull(data.Song);
        }

        [Test]
        public void Test_ToNative_EmptyChart()
        {
            var data = newProjectData();
            var chart = new ChartData();
            chart.KeyCount = 4;
            data.Charts = new ChartData[] { chart };

            var native = converter.ToNative(data);

            Assert.AreEqual(1, native.Charts.Count);
            Assert.IsEmpty(native.Charts[0].BPMs);
            Assert.AreEqual(4, native.Charts[0].Objects.Length);

            foreach (var list in native.Charts[0].Objects)
            {
                Assert.IsEmpty(list);
            }
        }

        [Test]
        public void Test_FromNative_EmptyChart()
        {
            var native = new Project();
            var chart = new Chart(4);
            native.Charts.Add(chart);

            var data = converter.FromNative(native);

            Assert.AreEqual(1, data.Charts.Length);
            Assert.AreEqual(chart.KeyCount, data.Charts[0].KeyCount);
            Assert.IsEmpty(data.Charts[0].BPMs);
            Assert.IsEmpty(data.Charts[0].Rows);
        }
    }
}