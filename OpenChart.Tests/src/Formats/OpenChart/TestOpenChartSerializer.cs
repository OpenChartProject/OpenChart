using NUnit.Framework;
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

        [Test]
        public void Test_Deserialize_MetadataRequired()
        {
            var data = "{}";
            var projectData = serializer.Deserialize(Encoding.UTF8.GetBytes(data));
        }

        [Test]
        public void Test_Deserialize_EmptyProject()
        {
            var data = @"
            {
                ""metadata"": {
                    ""version"": ""test-version""
                },
                ""charts"": [],
                ""song"": null
            }
            ";

            var pd = serializer.Deserialize(Encoding.UTF8.GetBytes(data));

            Assert.AreEqual("test-version", pd.Metadata.Version);
        }
    }
}