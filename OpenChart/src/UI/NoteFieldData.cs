using OpenChart.Charting;
using OpenChart.Charting.Properties;
using OpenChart.NoteSkins;
using System;

namespace OpenChart.UI
{
    /// <summary>
    /// Represents the data/state of a note field. This includes things like the Chart being
    /// displayed, note skin, scroll position, etc.
    /// </summary>
    public class NoteFieldData
    {
        /// <summary>
        /// Represents the current scroll state of the note field.
        /// </summary>
        public class ScrollState
        {
            NoteFieldData data;

            /// <summary>
            /// The scroll position (in beats).
            /// </summary>
            public Beat Beat { get; internal set; }

            /// <summary>
            /// The index of the BPM interval that the current scroll position is in.
            /// <seealso cref="OpenChart.Charting.BPMIntervalTracker.Intervals" />
            /// </summary>
            public uint IntervalIndex { get; internal set; }

            /// <summary>
            /// The scroll position (in pixels).
            /// </summary>
            public int Position => (int)Math.Floor(Time.Value * data.PixelsPerSecond);

            /// <summary>
            /// The scroll position (in seconds).
            /// </summary>
            public Time Time { get; internal set; }

            /// <summary>
            /// Creates a new ScrollState instance.
            /// </summary>
            public ScrollState(NoteFieldData data)
            {
                this.data = data;
                Beat = new Beat(0);
                Time = new Time(0);
            }
        }

        /// <summary>
        /// The Chart those note field is displaying.
        /// </summary>
        public readonly Chart Chart;

        /// <summary>
        /// The chart event bus for the displayed chart.
        /// </summary>
        public readonly ChartEventBus ChartEvents;

        /// <summary>
        /// The note skin to use for objects on the note field.
        /// </summary>
        public readonly KeyModeSkin NoteSkin;

        /// <summary>
        /// The number of pixels one second of time represents.
        /// </summary>
        public int PixelsPerSecond { get; private set; }

        /// <summary>
        /// The amount the note field has been scrolled by, in steps. 1 step = 1 mouse wheel tick.
        /// NOTE: Use `ScrollTop.Time` instead of this to get the current scroll time.
        /// </summary>
        public double RawScrollPosition { get; private set; }

        /// <summary>
        /// A multiplier for manipulating the scroll speed of the note field.
        /// </summary>
        public double ScrollScalar { get; private set; }

        /// <summary>
        /// The scroll state for the bottom of the note field viewport.
        /// </summary>
        public ScrollState ScrollBottom { get; private set; }

        /// <summary>
        /// The scroll state for the top of the note field viewport.
        /// </summary>
        public ScrollState ScrollTop { get; private set; }

        /// <summary>
        /// Creates a new NoteFieldData instance.
        /// </summary>
        /// <param name="chart">The chart this note field is displaying.</param>
        /// <param name="noteSkin">The note skin used by the note field.</param>
        public NoteFieldData(Chart chart, KeyModeSkin noteSkin, int pixelsPerSecond)
        {
            if (chart == null)
                throw new ArgumentNullException("Chart cannot be null.");
            else if (noteSkin == null)
                throw new ArgumentNullException("Note skin cannot be null.");
            else if (pixelsPerSecond <= 0)
                throw new ArgumentOutOfRangeException("Pixels per second must be greater than zero.");

            Chart = chart;
            NoteSkin = noteSkin;

            PixelsPerSecond = pixelsPerSecond;
            ScrollScalar = 0.25;

            ChartEvents = new ChartEventBus(Chart);
            ScrollBottom = new ScrollState(this);
            ScrollTop = new ScrollState(this);
        }

        /// <summary>
        /// Gets the Y-position (in pixels) that the given beat occurs at.
        /// </summary>
        public int GetPosition(Beat beat)
        {
            return GetPosition(Chart.BPMList.Time.BeatToTime(beat));
        }

        /// <summary>
        /// Gets the Y-position (in pixels) that the given time occurs at.
        /// </summary>
        public int GetPosition(Time time)
        {
            return (int)Math.Floor(time.Value * PixelsPerSecond);
        }

        /// <summary>
        /// Updates the ScrollTop and ScrollBottom properties with the new scroll state
        /// of the note field widget.
        ///
        /// Scrolling takes advantage of the fact that time is displayed linearly, unlike
        /// BPMs which can change. Because time is displayed linearly we can easily map
        /// between time <-> position.
        /// </summary>
        /// <param name="scrollDelta">The number of scroll steps the widget is scrolled.</param>
        /// <param name="widgetHeight">The height of the widget (in pixels).</param>
        public void UpdateScroll(double scrollDelta, int widgetHeight)
        {
            var oldPos = RawScrollPosition;
            RawScrollPosition += scrollDelta * ScrollScalar;

            if (RawScrollPosition < 0)
                RawScrollPosition = 0;

            // Nothing to update.
            if (oldPos == RawScrollPosition)
                return;

            // 1 step = 1 second.
            var time = new Time(RawScrollPosition);
            var y = time.Value * PixelsPerSecond;

            ScrollTop.Time = time;
            ScrollTop.IntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollTop.Time);
            ScrollTop.Beat = Chart.BPMList.Time.TimeToBeat(ScrollTop.Time, ScrollTop.IntervalIndex);

            var bottomTime = new Time(time.Value + ((double)widgetHeight / PixelsPerSecond));

            ScrollBottom.Time = bottomTime;
            ScrollBottom.IntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollBottom.Time);
            ScrollBottom.Beat = Chart.BPMList.Time.TimeToBeat(ScrollBottom.Time, ScrollBottom.IntervalIndex);
        }
    }
}
