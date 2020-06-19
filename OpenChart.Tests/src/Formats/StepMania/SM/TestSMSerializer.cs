using NUnit.Framework;
using OpenChart.Formats.StepMania.SM;

namespace OpenChart.Tests.Formats.StepMania.SM
{
    public class TestSMSerializer
    {
        SMSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new SMSerializer();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(",")]
        public void Test_ParseBPMs_NoBPMs(string val)
        {
            var bpms = serializer.ParseBPMs(val);
            Assert.AreEqual(0, bpms.Count);
        }

        [TestCase("100=0", 100, 0)]
        [TestCase("123.45=1.2345", 123.45, 1.2345)]
        [TestCase("0=-1", 0, -1)]
        public void Test_ParseBPMs_Single(string val, double beat, double bpm)
        {
            var bpms = serializer.ParseBPMs(val);
            Assert.AreEqual(1, bpms.Count);
            Assert.AreEqual(beat, bpms[0].Beat);
            Assert.AreEqual(bpm, bpms[0].Value);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(",")]
        public void Test_ParseBPMs_NoStops(string val)
        {
            var stops = serializer.ParseStops(val);
            Assert.AreEqual(0, stops.Count);
        }

        [TestCase("100=0", 100, 0)]
        [TestCase("123.45=1.2345", 123.45, 1.2345)]
        public void Test_ParseStops_Single(string val, double beat, double seconds)
        {
            var stops = serializer.ParseStops(val);
            Assert.AreEqual(1, stops.Count);
            Assert.AreEqual(beat, stops[0].Beat);
            Assert.AreEqual(seconds, stops[0].Seconds);
        }
    }
}
