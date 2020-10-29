using NUnit.Framework;
using OpenChart.Formats.StepMania.SM;
using OpenChart.Formats.StepMania.SM.Data;
using OpenChart.Formats.StepMania.SM.Enums;
using OpenChart.Formats.StepMania.SM.Exceptions;

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

        [Test]
        public void Test_ParseChartHeaders()
        {
            var chart = new Chart();

            FieldParser.ParseChartHeaders("dance-single:author:easy:5:", ref chart);

            Assert.AreEqual("dance-single", chart.ChartType.Name);
            Assert.AreEqual("author", chart.Author);
            Assert.AreEqual(ChartDifficulty.Easy, chart.Difficulty);
            Assert.AreEqual(5, chart.DifficultyRating);
        }

        [TestCase("")]
        [TestCase("     ")]
        public void Test_ParseNoteData_Empty(string data)
        {
            var measures = FieldParser.ParseNoteData(data, 4);
            Assert.AreEqual(0, measures.Count);
        }

        [TestCase("0", 4)]
        [TestCase("1111 111", 4)]
        public void Test_ParseNoteData_KeyCountMismatch(string data, int keyCount)
        {
            Assert.Throws<FieldFormatException>(
                () => FieldParser.ParseNoteData(data, keyCount)
            );
        }

        [TestCase("0000", 4, 1)]
        [TestCase("0000,", 4, 1)]
        [TestCase("0000,0000", 4, 2)]
        [TestCase("0000 0000 0000 0000 0000 0000 0000 0000", 4, 1)]
        public void Test_ParseNoteData_MeasureCountOK(string data, int keyCount, int expected)
        {
            var measures = FieldParser.ParseNoteData(data, keyCount);
            Assert.AreEqual(expected, measures.Count);
        }

        [Test]
        public void Test_ParseNoteData_SubDivisions()
        {
            // Taken from testdata/sample.sm
            var data = @"
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
            0000";

            var measures = FieldParser.ParseNoteData(data, 4);
            Assert.AreEqual(1, measures.Count);
            Assert.AreEqual(16, measures[0].Subdivisions);

            // Row 0
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.Tap,
                    NoteType.Tap,
                    NoteType.Tap,
                    NoteType.Tap,
                },
                measures[0].BeatRows[0].Notes
            );

            // Row 1
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.Tap,
                    NoteType.Empty,
                    NoteType.Empty,
                    NoteType.Empty,
                },
                measures[0].BeatRows[1].Notes
            );

            // Row 2
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.Empty,
                    NoteType.Tap,
                    NoteType.Empty,
                    NoteType.Empty,
                },
                measures[0].BeatRows[2].Notes
            );

            // Row 3
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.Empty,
                    NoteType.Empty,
                    NoteType.Tap,
                    NoteType.Empty,
                },
                measures[0].BeatRows[3].Notes
            );

            // Row 4
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.HoldHead,
                    NoteType.RollHead,
                    NoteType.Tap,
                    NoteType.Tap,
                },
                measures[0].BeatRows[4].Notes
            );

            // Row 5
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.Empty,
                    NoteType.Empty,
                    NoteType.Empty,
                    NoteType.Empty,
                },
                measures[0].BeatRows[5].Notes
            );

            // Row 6
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.Empty,
                    NoteType.Empty,
                    NoteType.Tap,
                    NoteType.Tap,
                },
                measures[0].BeatRows[6].Notes
            );

            // Row 7
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.Empty,
                    NoteType.Empty,
                    NoteType.Empty,
                    NoteType.Empty,
                },
                measures[0].BeatRows[7].Notes
            );

            // Row 8
            Assert.AreEqual(
                new NoteType[]{
                    NoteType.HoldRollTail,
                    NoteType.HoldRollTail,
                    NoteType.Mine,
                    NoteType.Mine,
                },
                measures[0].BeatRows[8].Notes
            );

            // Rows 9 - 15
            for (var i = 9; i < 16; i++)
            {
                Assert.AreEqual(
                    new NoteType[]{
                        NoteType.Empty,
                        NoteType.Empty,
                        NoteType.Empty,
                        NoteType.Empty,
                    },
                    measures[0].BeatRows[i].Notes
                );
            }
        }

        [Test]
        public void Test_ParseBeatRow()
        {
            var row = FieldParser.ParseBeatRow("0000", 4);
            Assert.AreEqual(NoteType.Empty, row.Notes[0]);
            Assert.AreEqual(NoteType.Empty, row.Notes[1]);
            Assert.AreEqual(NoteType.Empty, row.Notes[2]);
            Assert.AreEqual(NoteType.Empty, row.Notes[3]);

            row = FieldParser.ParseBeatRow("01234MKLF", 9);
            Assert.AreEqual(NoteType.Empty, row.Notes[0]);
            Assert.AreEqual(NoteType.Tap, row.Notes[1]);
            Assert.AreEqual(NoteType.HoldHead, row.Notes[2]);
            Assert.AreEqual(NoteType.HoldRollTail, row.Notes[3]);
            Assert.AreEqual(NoteType.RollHead, row.Notes[4]);
            Assert.AreEqual(NoteType.Mine, row.Notes[5]);
            Assert.AreEqual(NoteType.KeySound, row.Notes[6]);
            Assert.AreEqual(NoteType.Lift, row.Notes[7]);
            Assert.AreEqual(NoteType.Fake, row.Notes[8]);
        }
    }
}
