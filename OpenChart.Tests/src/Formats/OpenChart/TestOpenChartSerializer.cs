using NUnit.Framework;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using System.Text;

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

        [TestCase("{}")]
        [TestCase("{ \"metadata\": null }")]
        public void Test_Deserialize_MetadataRequired(string data)
        {
            Assert.Throws<SerializerException>(() => serializer.Deserialize(Encoding.UTF8.GetBytes(data)));
        }

        [TestCase("{ \"metadata\": {} }")]
        [TestCase("{ \"metadata\": { \"version\": null } }")]
        [TestCase("{ \"metadata\": { \"version\": \"\" } }")]
        public void Test_Deserialize_VersionRequired(string data)
        {
            Assert.Throws<SerializerException>(() => serializer.Deserialize(Encoding.UTF8.GetBytes(data)));
        }

        [TestCase(@"
            {
                ""metadata"": {
                    ""version"": ""test-version""
                }
            }
            ")]
        [TestCase(@"
            {
                ""metadata"": {
                    ""version"": ""test-version""
                },
                ""charts"": [],
                ""song"": null
            }
            ")]
        public void Test_Deserialize_EmptyProject(string data)
        {
            var pd = serializer.Deserialize(Encoding.UTF8.GetBytes(data));

            Assert.AreEqual("test-version", pd.Metadata.Version);
            Assert.IsEmpty(pd.Charts);
            Assert.IsNull(pd.Song);
        }

        [TestCase(@"
            {
                ""metadata"": {
                    ""version"": ""test-version""
                },
                ""charts"": [
                    {
                        ""keyCount"": 4,
                        ""author"": ""jessie"",
                        ""chartName"": ""test chart"",
                        ""rows"": []
                    }
                ],
                ""song"": null
            }
            ")]
        public void Test_Deserialize_EmptyChart(string data)
        {
            var pd = serializer.Deserialize(Encoding.UTF8.GetBytes(data));

            Assert.AreEqual(1, pd.Charts.Length);

            var chart = pd.Charts[0];

            Assert.AreEqual(4, chart.KeyCount.Value);
            Assert.AreEqual("jessie", chart.Author);
            Assert.AreEqual("test chart", chart.ChartName);
            Assert.IsEmpty(chart.Rows);
        }

        [Test]
        public void Test_Serialize_EmptyProject()
        {
            var expected = newProject();
            var actual = serializer.Deserialize(serializer.Serialize(expected));

            Assert.AreEqual(expected.Metadata.Version, actual.Metadata.Version);
            Assert.IsEmpty(actual.Charts);
            Assert.IsNull(actual.Song);
        }

        [Test]
        public void Test_Serialize_Song()
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
        public void Test_Serialize_EmptyChart()
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
            Assert.IsEmpty(actual.Charts[0].Rows);
        }
    }
}