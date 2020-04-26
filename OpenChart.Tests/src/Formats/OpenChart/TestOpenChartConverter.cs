using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Properties;
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
        public void Test_ToNative_EmptyProject()
        {
            var data = newProjectData();
            var native = converter.ToNative(data);

            Assert.IsEmpty(native.Charts);
            Assert.IsNull(native.SongMetadata);
        }

        [Test]
        public void Test_FromNative_EmptyProject()
        {
            var native = new Project();
            var data = converter.FromNative(native);

            Assert.AreEqual(OpenChartFormatHandler.Version, data.Metadata.Version);
            Assert.IsEmpty(data.Charts);
            Assert.IsNull(data.Song);
        }

        [Test]
        public void Test_ToNative_ProjectWithEmptyChart()
        {
            var data = newProjectData();
            var chart = new ChartData();
            data.Charts = new ChartData[] { chart };

            chart.KeyCount = 4;

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
        public void Test_FromNative_ProjectWithEmptyChart()
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

        [Test]
        public void Test_ToNative_ChartWithBPMData()
        {
            var data = newProjectData();
            var chart = new ChartData();
            data.Charts = new ChartData[] { chart };

            chart.KeyCount = 4;
            chart.BPMs = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10.5),
            };

            var native = converter.ToNative(data);

            Assert.AreEqual(chart.BPMs, native.Charts[0].BPMs.ToArray());
        }

        [Test]
        public void Test_FromNative_ChartWithBPMData()
        {
            var native = new Project();
            var chart = new Chart(4);
            native.Charts.Add(chart);

            chart.BPMs.AddMultiple(new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10.5),
            });

            var data = converter.FromNative(native);

            Assert.AreEqual(chart.BPMs.ToArray(), data.Charts[0].BPMs);
        }
    }
}
