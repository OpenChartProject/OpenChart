using NUnit.Framework;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using System;

namespace OpenChart.Tests.Formats.OpenChart.Data
{
    public class TestProjectMetadata
    {
        [TestCase(null)]
        [TestCase("")]
        public void Test_Validate_MissingVersion(string value)
        {
            var data = new ProjectMetadata { Version = value };
            Assert.Catch(() => data.Validate());
        }
    }
}
