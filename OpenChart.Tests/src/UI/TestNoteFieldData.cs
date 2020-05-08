using NUnit.Framework;
using OpenChart.Charting;
using NativeObjects = OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using OpenChart.UI;

namespace OpenChart.Tests.UI
{
    public class TestNoteFieldData
    {
        Chart Chart;
        NoteFieldData NoteFieldData;
        int KeyWidth;
        int PixelsPerSecond;
        Time TimeOffset;
        bool CenterObjectsOnBeatLine;

        [SetUp]
        public void SetUp()
        {
            KeyWidth = 64;
            PixelsPerSecond = 200;
            TimeOffset = 1.0;
            CenterObjectsOnBeatLine = false;

            Chart = new Chart(4);
            Chart.BPMList.BPMs.Add(new BPM(100, 0));
            NoteFieldData = new NoteFieldData(
                Chart,
                ToolKit.GetInstance().NoteSkin.GetKeyModeSkin(4),
                KeyWidth,
                PixelsPerSecond,
                TimeOffset,
                CenterObjectsOnBeatLine
            );
        }

        [Test]
        public void Test_Init()
        {
            Assert.AreEqual(Chart, NoteFieldData.Chart);
            Assert.AreEqual(KeyWidth, NoteFieldData.KeyWidth);
            Assert.AreEqual(PixelsPerSecond, NoteFieldData.PixelsPerSecond);
            Assert.AreEqual(TimeOffset.Value, NoteFieldData.TopTimeMargin.Value);
            Assert.AreEqual(CenterObjectsOnBeatLine, NoteFieldData.CenterObjectsOnBeatLines);

            Assert.NotNull(NoteFieldData.ChartEvents);
            Assert.NotNull(NoteFieldData.NoteSkin);

            Assert.NotNull(NoteFieldData.ScrollBottom);
            Assert.NotNull(NoteFieldData.ScrollTop);
        }

        [Test]
        public void Test_GetAbsoluteTime_WithTimeOffset()
        {
            Assert.IsTrue(0 > NoteFieldData.GetAbsoluteTime());
        }

        [Test]
        public void Test_GetAbsoluteTime_WithoutTimeOffset()
        {
            NoteFieldData.TopTimeMargin.Value = 0;
            Assert.AreEqual(0, NoteFieldData.GetAbsoluteTime());
        }

        [Test]
        public void Test_ChartLength_WhenChartIsEmpty()
        {
            Assert.AreEqual(
                Chart.BPMList.Time.BeatToTime(NoteFieldData.ExtraEndBeats),
                NoteFieldData.ChartLength
            );
        }

        [Test]
        public void Test_ChartLength_WhenChartIsNotEmpty()
        {
            var note = new NativeObjects.TapNote(0, 5);
            Chart.Objects[0].Add(note);

            Assert.AreEqual(
                Chart.BPMList.Time.BeatToTime(note.Beat.Value + NoteFieldData.ExtraEndBeats.Value),
                NoteFieldData.ChartLength
            );
        }

        [TestCase(-1)]
        [TestCase(1)]
        public void Test_OnScroll_ChangesScrollState(double deltaY)
        {
            NoteFieldData.OnScroll(10, 400);

            Beat topBeat = NoteFieldData.ScrollTop.Beat.Value;
            Time topTime = NoteFieldData.ScrollTop.Time.Value;
            Beat bottomBeat = NoteFieldData.ScrollBottom.Beat.Value;
            Time bottomTime = NoteFieldData.ScrollBottom.Time.Value;

            NoteFieldData.OnScroll(deltaY, 400);

            Assert.AreNotEqual(topBeat.Value, NoteFieldData.ScrollTop.Beat.Value);
            Assert.AreNotEqual(topTime.Value, NoteFieldData.ScrollTop.Time.Value);
            Assert.AreNotEqual(bottomBeat.Value, NoteFieldData.ScrollBottom.Beat.Value);
            Assert.AreNotEqual(bottomTime.Value, NoteFieldData.ScrollBottom.Time.Value);
        }

        [Test]
        public void Test_ScrollTop_CapsAtTimeOffset()
        {
            Assert.AreEqual(0, NoteFieldData.ScrollTop.Beat.Value);
            Assert.AreEqual(0, NoteFieldData.ScrollTop.Time.Value);
            Assert.AreEqual(0, NoteFieldData.ScrollTop.Position);

            var stepsScrolled = NoteFieldData.StepsScrolled;

            NoteFieldData.OnScroll(TimeOffset.Value * NoteFieldData.StepsPerSecond, 0);
            Assert.AreNotEqual(stepsScrolled, NoteFieldData.StepsScrolled);

            Assert.AreEqual(0, NoteFieldData.ScrollTop.Beat.Value);
            Assert.AreEqual(0, NoteFieldData.ScrollTop.Time.Value);
            Assert.AreEqual(0, NoteFieldData.ScrollTop.Position);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void Test_ScrollTop_PositionWithTimeOffset(double deltaY)
        {
            NoteFieldData.OnScroll(1, 0);
            NoteFieldData.OnScroll(deltaY, 0);

            Assert.AreNotEqual(
                NoteFieldData.ScrollTop.Position,
                NoteFieldData.ScrollTop.PositionWithOffset
            );
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void Test_ScrollTop_PositionWithoutTimeOffset(double deltaY)
        {
            NoteFieldData.TopTimeMargin = 0;
            NoteFieldData.OnScroll(1, 0);
            NoteFieldData.OnScroll(deltaY, 0);

            Assert.AreEqual(
                NoteFieldData.ScrollTop.Position,
                NoteFieldData.ScrollTop.PositionWithOffset
            );
        }
    }
}
