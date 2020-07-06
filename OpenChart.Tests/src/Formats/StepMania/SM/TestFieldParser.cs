using NUnit.Framework;
using OpenChart.Formats.StepMania.SM;
using OpenChart.Formats.StepMania.SM.Enums;

namespace OpenChart.Tests.Formats.StepMania.SM
{
    public class TestFieldParser
    {
        [Test]
        public void Test_ParseDisplayBPM_Fixed()
        {
            var display = FieldParser.ParseDisplayBPM("120");
            Assert.AreEqual(DisplayBPMType.Fixed, display.Type);
            Assert.AreEqual(120, display.Min);
        }

        [Test]
        public void Test_ParseDisplayBPM_Random()
        {
            var display = FieldParser.ParseDisplayBPM("*");
            Assert.AreEqual(DisplayBPMType.Random, display.Type);
        }

        [Test]
        public void Test_ParseDisplayBPM_Range()
        {
            var display = FieldParser.ParseDisplayBPM("60:120");
            Assert.AreEqual(DisplayBPMType.Range, display.Type);
            Assert.AreEqual(60, display.Min);
            Assert.AreEqual(120, display.Max);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(",")]
        public void Test_ParseBPMList_Empty(string val)
        {
            var bpms = FieldParser.ParseBPMList(val);
            Assert.AreEqual(0, bpms.Count);
        }

        [TestCase("100=0", 100, 0)]
        [TestCase("123.45=1.2345", 123.45, 1.2345)]
        [TestCase("0=-1", 0, -1)]
        public void Test_ParseBPMList_Single(string val, double beat, double bpm)
        {
            var bpms = FieldParser.ParseBPMList(val);
            Assert.AreEqual(1, bpms.Count);
            Assert.AreEqual(beat, bpms[0].Beat);
            Assert.AreEqual(bpm, bpms[0].Value);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(",")]
        public void Test_ParseStopList_Empty(string val)
        {
            var stops = FieldParser.ParseStopList(val);
            Assert.AreEqual(0, stops.Count);
        }

        [TestCase("100=0", 100, 0)]
        [TestCase("123.45=1.2345", 123.45, 1.2345)]
        public void Test_ParseStopList_Single(string val, double beat, double seconds)
        {
            var stops = FieldParser.ParseStopList(val);
            Assert.AreEqual(1, stops.Count);
            Assert.AreEqual(beat, stops[0].Beat);
            Assert.AreEqual(seconds, stops[0].Seconds);
        }

        [TestCase("Beginner", ChartDifficulty.Beginner)]
        [TestCase("Easy", ChartDifficulty.Easy)]
        [TestCase("Medium", ChartDifficulty.Medium)]
        [TestCase("Hard", ChartDifficulty.Hard)]
        [TestCase("Insane", ChartDifficulty.Insane)]
        [TestCase("Edit", ChartDifficulty.Edit)]
        [TestCase("foo", ChartDifficulty.Edit)]
        public void Test_ParseChartDifficulty(string difficulty, ChartDifficulty expected)
        {
            var actual = FieldParser.ParseChartDifficulty(difficulty);
            Assert.AreEqual(expected, actual);
        }
    }
}
