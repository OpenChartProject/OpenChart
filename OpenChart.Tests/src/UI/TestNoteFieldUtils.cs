using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Properties;
using NativeObjects = OpenChart.Charting.Objects;
using OpenChart.UI;
using Widgets = OpenChart.UI.Widgets;
using System;

namespace OpenChart.Tests.UI
{
    public class TestNoteFieldUtils
    {
        class DummyObject : NativeObjects.BaseObject
        {
            public DummyObject(KeyIndex keyIndex, Beat beat) : base(keyIndex, beat) { }
        }

        Chart Chart;
        NoteFieldData NoteFieldData;
        const int KeyWidth = 64;
        const int PixelsPerSecond = 200;
        const double TimeOffset = 1.0;

        [SetUp]
        public void SetUp()
        {
            Chart = new Chart(4);
            Chart.BPMList.BPMs.Add(new BPM(100, 0));
            NoteFieldData = new NoteFieldData(
                Chart,
                ToolKit.GetInstance().NoteSkin.GetKeyModeSkin(4),
                KeyWidth,
                PixelsPerSecond,
                TimeOffset,
                false
            );
        }

        [Test]
        public void Test_CreateWidgetForChartObject_InvalidObject()
        {
            Assert.Throws<Exception>(() => NoteFieldUtils.CreateWidgetForChartObject(
                NoteFieldData,
                new DummyObject(0, 0),
                ToolKit.GetInstance().NoteSkin.GetKeyModeSkin(4).Keys[0]
            ));
        }

        [Test]
        public void Test_CreateWidgetForChartObject_HoldNote()
        {
            var note = new NativeObjects.HoldNote(0, 0, 1);
            var uiObj = NoteFieldUtils.CreateWidgetForChartObject(
                NoteFieldData,
                note,
                ToolKit.GetInstance().NoteSkin.GetKeyModeSkin(4).Keys[note.KeyIndex.Value]
            );

            Assert.AreSame(note, uiObj.GetChartObject());
            Assert.IsInstanceOf(typeof(Widgets.HoldNote), uiObj);
        }

        [Test]
        public void Test_CreateWidgetForChartObject_TapNote()
        {
            var note = new NativeObjects.TapNote(0, 0);
            var uiObj = NoteFieldUtils.CreateWidgetForChartObject(
                NoteFieldData,
                note,
                ToolKit.GetInstance().NoteSkin.GetKeyModeSkin(4).Keys[note.KeyIndex.Value]
            );

            Assert.AreSame(note, uiObj.GetChartObject());
            Assert.IsInstanceOf(typeof(Widgets.TapNote), uiObj);
        }
    }
}
