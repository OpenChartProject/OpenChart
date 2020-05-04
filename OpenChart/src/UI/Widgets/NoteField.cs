using Gdk;
using Gtk;
using OpenChart.Charting;
using OpenChart.Charting.Properties;
using OpenChart.NoteSkins;
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

        public readonly RGBA BackgroundColor = new RGBA
        {
            Red = 0.1,
            Green = 0.1,
            Blue = 0.1,
            Alpha = 1.0,
        };

        public const int VerticalMargin = 100;
        public const int TimeSpacing = 200;

        int _columnWidth;
        public int ColumnWidth
        {
            get => _columnWidth;
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Width must be greater than zero.");

                _columnWidth = value;

                NoteSkin.ScaleToNoteFieldColumnWidth(_columnWidth);
            }
        }

        public int NoteFieldWidth => ColumnWidth * Chart.KeyCount.Value;

        public int ViewportTopY => GetYPosOfTime(ScrollTime);
        public int ViewportBottomY => ViewportTopY + AllocatedHeight;

        public Beat ScrollBeat { get; private set; }
        public uint ScrollIntervalIndex { get; private set; }
        public Time ScrollTime { get; private set; }

        public NoteFieldKey[] Keys;
        public readonly Chart Chart;
        public ChartEventBus ChartEvents { get; private set; }
        public readonly KeyModeSkin NoteSkin;

        public NoteField(Chart chart, KeyModeSkin noteSkin, int columnWidth) : base(null, null)
        {
            if (chart == null)
                throw new ArgumentNullException("Chart cannot be null.");
            else if (noteSkin == null)
                throw new ArgumentNullException("Note skin cannot be null.");

            NoteSkin = noteSkin;
            ColumnWidth = columnWidth;

            Chart = chart;
            ChartEvents = new ChartEventBus(Chart);

            ScrollTime = new Time(0);
            widgetStack = new List<Widget>();
            beatLines = new BeatLines(this);
            keyContainer = new HBox();
            Keys = new NoteFieldKey[Chart.KeyCount.Value];

            for (var i = 0; i < Chart.KeyCount.Value; i++)
            {
                Keys[i] = new NoteFieldKey(this, i, NoteSkin);
                keyContainer.Add(Keys[i]);
            }

            Add(beatLines);
            Add(keyContainer);

            ScrollEvent += onScroll;

            SizeAllocated += (o, e) =>
            {
                beatLines.SetSizeRequest(NoteFieldWidth, e.Allocation.Height);
            };

            ChartEvents.ObjectAdded += (o, e) =>
            {
                Keys[e.Object.KeyIndex.Value].Add(e.Object);
            };
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
            var bg = BackgroundColor;

            cr.SetSourceRGBA(bg.Red, bg.Green, bg.Blue, bg.Alpha);
            cr.Paint();

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
