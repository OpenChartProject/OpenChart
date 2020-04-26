using NUnit.Framework;
using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Formats.OpenChart.Version0_1.Objects;

namespace OpenChart.Tests.Formats.OpenChart
{
    public class TestOpenChartSerializer
    {
        OpenChartSerializer serializer;

        private ProjectData newProject()
        {
            var project = new ProjectData();
            project.Metadata.Version = "test-version";
            return project;
        }

        [SetUp]
        public void SetUp()
        {
            serializer = new OpenChartSerializer();
        }

        [Test]
        public void Test_Deserialize_EmptyProject()
        {
            var expected = newProject();
            var actual = serializer.Deserialize(serializer.Serialize(expected));

            Assert.AreEqual(expected.Metadata.Version, actual.Metadata.Version);
            Assert.IsEmpty(actual.Charts);
            Assert.IsNull(actual.Song);
        }

        [Test]
        public void Test_Deserialize_Song()
        {
            var expected = newProject();
            expected.Song = new SongData
            {
                Artist = "Some Cool Artist",
                Title = "Some Cool Song",
                Path = "audio.mp3",
            };

            var actual = serializer.Deserialize(serializer.Serialize(expected));

            Assert.AreEqual(actual.Song.Artist, expected.Song.Artist);
            Assert.AreEqual(actual.Song.Title, expected.Song.Title);
            Assert.AreEqual(actual.Song.Path, expected.Song.Path);
        }

        [Test]
        public void Test_Deserialize_EmptyChart()
        {
            var expected = newProject();
            var chart = new ChartData
            {
                Author = "Jessie",
                ChartName = "My chart",
                KeyCount = 4,
            };
            expected.Charts = new ChartData[] { chart };

            var actual = serializer.Deserialize(serializer.Serialize(expected));

            Assert.AreEqual(1, actual.Charts.Length);
            Assert.AreEqual(chart.Author, actual.Charts[0].Author);
            Assert.AreEqual(chart.ChartName, actual.Charts[0].ChartName);
            Assert.AreEqual(chart.KeyCount, actual.Charts[0].KeyCount);
            Assert.IsEmpty(actual.Charts[0].BPMs);
            Assert.IsEmpty(actual.Charts[0].Rows);
        }

        [Test]
        public void Test_Deserialize_BPMs()
        {
            var expected = newProject();
            var chart = new ChartData
            {
                Author = "Jessie",
                ChartName = "My chart",
                KeyCount = 4,
            };
            expected.Charts = new ChartData[] { chart };

            chart.BPMs = new BPM[] {
                new BPM(100, 0),
                new BPM(150, 10.5),
            };

            var actual = serializer.Deserialize(serializer.Serialize(expected));

            Assert.AreEqual(2, actual.Charts[0].BPMs.Length);
            Assert.AreEqual(chart.BPMs, actual.Charts[0].BPMs);
        }

        [Test]
        public void Test_Deserialize_BeatRows()
        {
            var expected = newProject();
            var chart = new ChartData
            {
                Author = "Jessie",
                ChartName = "My chart",
                KeyCount = 4,
            };
            expected.Charts = new ChartData[] { chart };

            chart.Rows = new BeatRowData[]
            {
                new BeatRowData
                {
                    Beat = 0,
                    Objects = new IChartObject[]
                    {
                        new TapNote(),
                        new TapNote(),
                        new TapNote(),
                        new TapNote(),
                    },
                },

                new BeatRowData
                {
                    Beat = 1,
                    Objects = new IChartObject[]
                    {
                        null,
                        null,
                        null,
                        new TapNote(),
                    },
                },

                new BeatRowData
                {
                    Beat = 2,
                    Objects = new IChartObject[]
                    {
                        new HoldNote { Length = 1 },
                        new HoldNote { Length = 2 },
                        new HoldNote { Length = 3 },
                        new HoldNote { Length = 4 },
                    },
                },

                new BeatRowData
                {
                    Beat = 10,
                    Objects = new IChartObject[]
                    {
                        new TapNote(),
                        new TapNote(),
                        new TapNote(),
                        new TapNote(),
                    },
                },
            };

            var actual = serializer.Deserialize(serializer.Serialize(expected));

            Assert.AreEqual(chart.Rows, actual.Charts[0].Rows);

            // Just as a sanity check
            chart.Rows[0].Beat.Value += 0.1;
            Assert.AreNotEqual(chart.Rows, actual.Charts[0].Rows);
        }
    }
}
