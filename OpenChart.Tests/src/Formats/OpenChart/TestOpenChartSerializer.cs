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
        public void Test_Deserialize_MetaData()
        {
            var data = @"
            {
                ""metadata"": {
                    ""keyCount"": 4,
                    ""version"": ""0.1"",
                    ""author"": ""Jessie"",
                    ""chartName"": ""I hope this test passes""
                }
            }
            ";

            var fd = serializer.Deserialize(Encoding.UTF8.GetBytes(data));

            Assert.AreEqual(4, fd.Metadata.KeyCount);
            Assert.AreEqual("0.1", fd.Metadata.Version);
            Assert.AreEqual("Jessie", fd.Metadata.Author);
            Assert.AreEqual("I hope this test passes", fd.Metadata.ChartName);
        }
    }
}