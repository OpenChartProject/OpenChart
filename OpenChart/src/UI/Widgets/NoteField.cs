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

        public const double TimeSpacing = 100;
        public Beat ScrollBeat { get; private set; }
        public BPMInterval ScrollInterval { get; private set; }
        public double ScrollSeconds { get; private set; }

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

            ScrollSeconds += e.Event.DeltaY;

            if (ScrollSeconds < 0)
                ScrollSeconds = 0;

            if (ScrollSeconds != oldY)
            {
                var index = Chart.BPMList.Time.GetIndexAtTime(ScrollSeconds);
                ScrollInterval = Chart.BPMList.Time.Intervals[index];
                ScrollBeat = Chart.BPMList.Time.TimeToBeat(ScrollSeconds, fromIndex: index);

                foreach (var widget in widgetStack)
                {
                    Move(widget, 0, (int)Math.Floor(ScrollSeconds * TimeSpacing));
                }
            }
        }
    }
}
