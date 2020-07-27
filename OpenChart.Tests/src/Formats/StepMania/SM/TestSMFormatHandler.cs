using NUnit.Framework;
using OpenChart.Formats.StepMania.SM;
using System.IO;

namespace OpenChart.Tests.Formats.StepMania.SM
{
    public class TestSMFormatHandler
    {
        SMFormatHandler handler;

        [SetUp]
        public void SetUp()
        {
            handler = new SMFormatHandler();
        }

        [Test]
        public void Test_Read_SampleFile()
        {
            System.Environment.GetEnvironmentVariables();
            var data = ToolKit.GetInstance().ReadTestDataFile("sample.sm");
            var reader = new StreamReader(new MemoryStream(data));
            var p = handler.Read(reader);
        }
    }
}
