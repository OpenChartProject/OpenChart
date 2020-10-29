using NUnit.Framework;
using OpenChart.Formats.StepMania.SM;
using Objects = OpenChart.Charting.Objects;
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

            Assert.AreEqual(4, c.KeyCount.Value);
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
            var c0 = c.Objects[0].ToArray();

            Assert.AreEqual(3, c0.Length);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c0[0]);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c0[1]);
            Assert.IsInstanceOf(typeof(Objects.HoldNote), c0[2]);
            Assert.AreEqual(2, ((Objects.HoldNote)c0[2]).EndBeat.Value);

            // Second column
            var c1 = c.Objects[1].ToArray();

            Assert.AreEqual(3, c1.Length);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c1[0]);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c1[1]);
            Assert.IsInstanceOf(typeof(Objects.HoldNote), c1[2]);
            Assert.AreEqual(2, ((Objects.HoldNote)c1[2]).EndBeat.Value);

            // Third column
            var c2 = c.Objects[2].ToArray();

            Assert.AreEqual(4, c2.Length);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c2[0]);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c2[1]);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c2[2]);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c2[3]);

            // Fourth column
            var c3 = c.Objects[3].ToArray();

            Assert.AreEqual(3, c3.Length);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c3[0]);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c3[1]);
            Assert.IsInstanceOf(typeof(Objects.TapNote), c3[2]);
        }
    }
}
