using NUnit.Framework;
using OpenChart.Formats.StepMania.SM;
using OpenChart.Formats.StepMania.SM.Data;
using Enums = OpenChart.Formats.StepMania.SM.Enums;
using Exceptions = OpenChart.Formats.StepMania.SM.Exceptions;

namespace OpenChart.Tests.Formats.StepMania.SM
{
    public class TestSMConverter
    {
        SMConverter converter;
        StepFileData sfd;

        [SetUp]
        public void SetUp()
        {
            converter = new SMConverter();
            sfd = new StepFileData();
            sfd.PlayData.BPMs.Add(new BPM(0, 120));
        }

        [Test]
        public void Test_ToNative_MissingBPMData()
        {
            sfd.PlayData.BPMs.Clear();
            Assert.Throws<Exceptions.NoBPMException>(() => converter.ToNative(sfd));
        }

        [Test]
        public void Test_ToNative_ProjectMetadata()
        {
            sfd.SongData.Title = "title";
            sfd.SongData.Artist = "artist";
            sfd.SongData.Music = "audio.ogg";

            var p = converter.ToNative(sfd);
            Assert.AreEqual(sfd.SongData.Title, p.Name);
            Assert.AreEqual(sfd.SongData.Title, p.SongMetadata.Title);
            Assert.AreEqual(sfd.SongData.Artist, p.SongMetadata.Artist);
            Assert.AreEqual(sfd.SongData.Music, p.SongMetadata.AudioFilePath);
        }

        [Test]
        public void Test_ToNative_NoCharts()
        {
            var p = converter.ToNative(sfd);
            Assert.IsEmpty(p.Charts);
        }

        [Test]
        public void Test_ToNative_BPMs()
        {
            sfd.PlayData.BPMs.Clear();
            sfd.PlayData.BPMs.Add(new BPM(0, 100));
            sfd.PlayData.BPMs.Add(new BPM(1, 123.45));
            sfd.PlayData.BPMs.Add(new BPM(2.5, 300));

            var chart = new Chart();
            chart.ChartType = Enums.ChartType.Get("dance-single");
            sfd.Charts.Add(chart);

            var p = converter.ToNative(sfd);
            var actual = p.Charts[0].BPMList.BPMs.ToArray();

            Assert.AreEqual(sfd.PlayData.BPMs.Count, actual.Length);
            Assert.AreEqual(sfd.PlayData.BPMs[0].Value, actual[0].Value);
            Assert.AreEqual(sfd.PlayData.BPMs[0].Beat, actual[0].Beat.Value);
            Assert.AreEqual(sfd.PlayData.BPMs[1].Value, actual[1].Value);
            Assert.AreEqual(sfd.PlayData.BPMs[1].Beat, actual[1].Beat.Value);
            Assert.AreEqual(sfd.PlayData.BPMs[2].Value, actual[2].Value);
            Assert.AreEqual(sfd.PlayData.BPMs[2].Beat, actual[2].Beat.Value);
        }

        [Test]
        public void Test_ToNative_MultipleCharts()
        {
            sfd.Charts.Add(new Chart { ChartType = Enums.ChartType.Get("dance-single") });
            sfd.Charts.Add(new Chart { ChartType = Enums.ChartType.Get("dance-single") });
            sfd.Charts.Add(new Chart { ChartType = Enums.ChartType.Get("dance-single") });

            var p = converter.ToNative(sfd);
            Assert.AreEqual(sfd.Charts.Count, p.Charts.Count);
        }
    }
}
