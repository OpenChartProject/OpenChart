using NUnit.Framework;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using System;

namespace OpenChart.Tests.Formats.OpenChart.Data
{
    public class TestProjectData
    {
        [Test]
        public void Test_Init()
        {
            var data = new ProjectData();
            Assert.IsEmpty(data.Charts);
            Assert.NotNull(data.Metadata);
        }

        [Test]
        public void Test_Validate_NullMetadata()
        {
            var data = new ProjectData();

            // I don't know why this would ever happen but ¯\_(ツ)_/¯
            data.Metadata = null;

            try
            {
                data.Validate();
                Assert.Fail("Expected Validate method to throw an exception.");
            }
            catch { }
        }
    }
}
