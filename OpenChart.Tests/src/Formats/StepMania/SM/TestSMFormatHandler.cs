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
        public void Test_Read_SampleFile_MetaData()
        {
            var data = ToolKit.GetInstance().ReadTestDataFile("sample.sm");
            var reader = new StreamReader(new MemoryStream(data));
            var p = handler.Read(reader);

            Assert.AreEqual("Sample Title", p.Name);
            Assert.AreEqual("Sample Artist", p.SongMetadata.Artist);
            Assert.AreEqual("Sample Title", p.SongMetadata.Title);
            Assert.AreEqual("audio.wav", p.SongMetadata.AudioFilePath);
            Assert.AreEqual(1, p.Charts.Count);

            var c = p.Charts[0];
            var bpms = c.BPMList.BPMs.ToArray();

            Assert.AreEqual(1, bpms.Length);
            Assert.AreEqual(0.0, bpms[0].Beat.Value);
            Assert.AreEqual(120.0, bpms[0].Value);

            Assert.AreEqual("Jessie", c.Author);
            Assert.AreEqual("Sample Title", c.ChartName);
        }

        [Test]
        public void Test_Read_SampleFile_NoteData()
        {
            /*
            Measure data:

                1111
                1000
                0100
                0010
                2411
                0000
                0011
                0000
                33MM
                0000
                0000
                0000
                0000
                0000
                0000
                0000
            */

            var data = ToolKit.GetInstance().ReadTestDataFile("sample.sm");
            var reader = new StreamReader(new MemoryStream(data));
            var p = handler.Read(reader);
            var c = p.Charts[0];

            // First column
            Assert.AreEqual(3, c.Objects[0].Count);

            // Second column
            Assert.AreEqual(3, c.Objects[1].Count);

            // Third column
            Assert.AreEqual(3, c.Objects[2].Count);

            // Fourth column
            Assert.AreEqual(3, c.Objects[3].Count);
        }
    }
}
