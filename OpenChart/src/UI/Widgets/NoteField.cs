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

        const double scrollFactor = 0.2;

        public const int VerticalMargin = 100;
        public const int TimeSpacing = 200;

        public int ViewportTopY => GetYPosOfTime(ScrollTime);
        public int ViewportBottomY => ViewportTopY + AllocatedHeight;

        public Beat ScrollBeat { get; private set; }
        public uint ScrollIntervalIndex { get; private set; }
        public Time ScrollTime { get; private set; }

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

            SizeAllocated += (o, e) =>
            {
                beatLines.SetSizeRequest(keyContainer.AllocatedWidth, e.Allocation.Height);
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

        public int GetYPosOfTime(Time time)
        {
            return (int)Math.Floor(time.Value * TimeSpacing);
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
            var newY = ScrollTime.Value + (e.Event.DeltaY * scrollFactor);

            if (newY < 0)
                newY = 0;

            if (ScrollTime.Value != newY)
            {
                ScrollTime.Value = newY;
                ScrollIntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollTime);
                ScrollBeat = Chart.BPMList.Time.TimeToBeat(ScrollTime.Value, fromIndex: ScrollIntervalIndex);

                foreach (var widget in widgetStack)
                {
                    scrollWidget(widget);
                }
            }
        }

        private void scrollWidget(Widget widget)
        {
            Move(widget, 0, 0 - GetYPosOfTime(ScrollTime.Value));
        }
    }
}
