using Gtk;
using OpenChart.Charting;
using OpenChart.Charting.Properties;
using System;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    public class NoteField : Layout
    {
        HBox keyContainer;
        BeatLines beatLines;
        List<Widget> widgetStack;

        const double scrollFactor = 0.5;

        public const int VerticalMargin = 100;
        public const int TimeSpacing = 200;

        public Beat ScrollBeat { get; private set; }
        public double ScrollSeconds { get; private set; }
        public BPMInterval ScrollInterval { get => Chart.BPMList.Time.Intervals[ScrollIntervalIndex]; }
        public uint ScrollIntervalIndex { get; private set; }

        public NoteFieldKey[] Keys;
        public Chart Chart { get; private set; }

        public NoteField(Chart chart) : base(null, null)
        {
            Chart = chart;

            widgetStack = new List<Widget>();
            beatLines = new BeatLines { NoteField = this };
            keyContainer = new HBox();
            Keys = new NoteFieldKey[Chart.KeyCount.Value];

            for (var i = 0; i < Chart.KeyCount.Value; i++)
            {
                Keys[i] = new NoteFieldKey();
                keyContainer.Add(Keys[i]);
            }

            Add(beatLines);
            Add(keyContainer);

            keyContainer.SizeAllocated += (o, e) =>
            {
                beatLines.SetAllocation(e.Allocation);
            };

            ScrollEvent += onScroll;
        }

        public void Add(IChartObject obj)
        {
            var keyIndex = obj.GetChartObject().KeyIndex.Value;

            if (keyIndex >= Chart.KeyCount.Value)
                throw new ArgumentOutOfRangeException(
                    "The chart object's key index is out of range for the notefield's key count."
                );

            Keys[keyIndex].Add(obj);
        }

        public new void Add(Widget widget)
        {
            widgetStack.Add(widget);
            base.Add(widget);
            scrollWidget(widget);
        }

        public int GetYPosOfTime(double seconds)
        {
            return (int)Math.Floor(seconds * TimeSpacing);
        }

        protected override bool OnDrawn(Cairo.Context cr)
        {
            foreach (var widget in widgetStack)
            {
                PropagateDraw(widget, cr);
            }

            return true;
        }

        private void onScroll(object o, ScrollEventArgs e)
        {
            var oldY = ScrollSeconds;

            ScrollSeconds += e.Event.DeltaY * scrollFactor;

            if (ScrollSeconds < 0)
                ScrollSeconds = 0;

            if (ScrollSeconds != oldY)
            {
                ScrollIntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollSeconds);
                ScrollBeat = Chart.BPMList.Time.TimeToBeat(ScrollSeconds, fromIndex: ScrollIntervalIndex);

                foreach (var widget in widgetStack)
                {
                    scrollWidget(widget);
                }
            }
        }

        private void scrollWidget(Widget widget)
        {
            Move(widget, 0, GetYPosOfTime(ScrollSeconds));
        }
    }
}
