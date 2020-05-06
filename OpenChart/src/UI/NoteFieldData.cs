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
        /// Represents the current scroll state of the note field viewport.
        /// </summary>
        public class ScrollState
        {
            NoteFieldData data;
            internal int positionOffset;

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
            public int Position => (int)Math.Round(Time.Value * data.PixelsPerSecond);

            public int PositionWithOffset => Position - positionOffset;

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
        /// The raw number of input scroll "ticks" the note field has been scrolled by.
        /// </summary>
        double rawInputStepsScrolled;
        int oldViewportHeight;

        /// <summary>
        /// How much time 1 scroll step represents.
        /// </summary>
        int stepsPerSecond = 1;

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
        /// A multiplier for manipulating the scroll speed of the note field.
        /// </summary>
        public double ScrollScalar { get; private set; }

        /// <summary>
        /// The scroll state for the bottom of the note field viewport.
        ///
        /// <seealso cref="UpdateScroll" />
        /// </summary>
        public ScrollState ScrollBottom { get; private set; }

        /// <summary>
        /// The scroll state for the top of the note field viewport.
        ///
        /// <seealso cref="UpdateScroll" />
        /// </summary>
        public ScrollState ScrollTop { get; private set; }


        /// <summary>
        /// The amount of time to offset the note field components by. This is to allow for
        /// a margin so that beat #0 is not right up against the edge of the widget.
        /// </summary>
        public Time TimeOffset { get; private set; }

        /// <summary>
        /// The time offset, in pixels.
        /// </summary>
        public int TimeOffsetPosition => (int)Math.Round(TimeOffset.Value * PixelsPerSecond);

        /// <summary>
        /// Creates a new NoteFieldData instance.
        /// </summary>
        /// <param name="chart">The chart this note field is displaying.</param>
        /// <param name="noteSkin">The note skin used by the note field.</param>
        public NoteFieldData(Chart chart, KeyModeSkin noteSkin, int pixelsPerSecond, Time timeOffset)
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
            TimeOffset = timeOffset;

            ChartEvents = new ChartEventBus(Chart);
            ScrollBottom = new ScrollState(this);
            ScrollTop = new ScrollState(this);

            ScrollTop.positionOffset = TimeOffsetPosition;
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
        /// ScrollTop and ScrollBottom represent the note field's position, relative to
        /// the top and bottom of the viewport. The viewport is the visible part of the
        /// widget on screen (i.e. the widget's size).
        ///
        /// To support margins so that the top and bottom of the note field aren't cut off,
        /// you can actually scroll a bit past the note field in either direction, so long
        /// as it stays on screen.
        ///
        /// One caveat with this is the fact that Beat and Time objects can't have a negative
        /// value. So, for example, you can't technically scroll to a beat/time "before" the
        /// beginning of the chart because the concept of a negative beat/time doesn't exist.
        ///
        /// The solution to this is to simply cap the Beat/Time, but still change the Position.
        /// When scrolled before the beginning of the chart, the position is negative, but the
        /// Beat/Time remain at 0. In a sense, what this is doing is pretending the viewport
        /// is smaller than it really is when we're beyond the bounds of the chart.
        /// </summary>
        /// <param name="scrollDelta">The number of scroll steps the widget is scrolled.</param>
        /// <param name="viewportHeight">The height of the viewport (in pixels).</param>
        /// <returns>True if the scroll position was changed.</returns>
        public bool UpdateScroll(double scrollDelta, int viewportHeight)
        {
            var oldSteps = rawInputStepsScrolled;
            rawInputStepsScrolled += scrollDelta * ScrollScalar;

            if (rawInputStepsScrolled < 0)
                rawInputStepsScrolled = 0;

            // Nothing changed.
            if (oldSteps == rawInputStepsScrolled && viewportHeight == oldViewportHeight)
                return false;

            oldViewportHeight = viewportHeight;

            var absTime = (rawInputStepsScrolled * stepsPerSecond) - TimeOffset.Value;

            if (absTime < 0)
                ScrollTop.positionOffset = (int)Math.Round(Math.Abs(absTime) * PixelsPerSecond);
            else
                ScrollTop.positionOffset = 0;

            ScrollTop.Time = Math.Max(absTime, 0);
            ScrollTop.IntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollTop.Time);
            ScrollTop.Beat = Chart.BPMList.Time.TimeToBeat(ScrollTop.Time, ScrollTop.IntervalIndex);

            ScrollBottom.Time = ScrollTop.Time.Value + Math.Floor((double)viewportHeight / PixelsPerSecond);
            ScrollBottom.IntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollBottom.Time);
            ScrollBottom.Beat = Chart.BPMList.Time.TimeToBeat(ScrollBottom.Time, ScrollBottom.IntervalIndex);

            return true;
        }
    }
}
