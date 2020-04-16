using NUnit.Framework;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using System.Text;

namespace OpenChart.Tests.Formats.OpenChart
{
    public class TestOpenChartSerializer
    {
        OpenChartSerializer serializer;

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
    }
}
