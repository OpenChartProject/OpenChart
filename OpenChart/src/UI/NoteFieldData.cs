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

            /// <summary>
            /// The scroll position, including the offset (in pixels). Used by the note field
            /// to offset widgets.
            ///
            /// You are probably looking for <see cref="Position" />
            /// </summary>
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
        double stepsScrolled;
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
        /// The height of the chart, in pixels.
        /// </summary>
        public int ChartHeight => (int)Math.Ceiling(ChartLength.Value * PixelsPerSecond);

        /// <summary>
        /// The length of the chart to display, in seconds.
        ///
        /// TODO: This should be cached and only reran when the chart changes.
        /// </summary>
        public Time ChartLength
        {
            get
            {
                var beat = Chart.GetLastObject()?.Beat ?? 0;
                return Chart.BPMList.Time.BeatToTime(beat.Value + ExtraEndBeats.Value);
            }
        }

        /// <summary>
        /// The chart event bus for the displayed chart.
        /// </summary>
        public readonly ChartEventBus ChartEvents;

        /// <summary>
        /// The number of extra beats to add to the end of the chart.
        /// </summary>
        /// <value></value>
        public Beat ExtraEndBeats { get; private set; }

        /// <summary>
        /// The width (in pixels) of each note field key widget.
        /// </summary>
        public int KeyWidth { get; private set; }

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
        /// The margin (in seconds) applied to the top of the note field. This is the amount of
        /// time (and therefore, pixels) that the user can scroll beyond the beginning of hte chart.
        /// </summary>
        public Time TopTimeMargin { get; private set; }

        /// <summary>
        /// Creates a new NoteFieldData instance.
        /// </summary>
        /// <param name="chart">The chart this note field is displaying.</param>
        /// <param name="noteSkin">The note skin used by the note field.</param>
        /// <param name="keyWidth">The width of each key in the note field.</param>
        /// <param name="pixelsPerSecond">How many pixels 1 second represents.</param>
        /// <param name="timeOffset">How much to offset the top of the screen.</param>
        public NoteFieldData(
            Chart chart,
            KeyModeSkin noteSkin,
            int keyWidth,
            int pixelsPerSecond,
            Time timeOffset
        )
        {
            if (chart == null)
                throw new ArgumentNullException("Chart cannot be null.");
            else if (noteSkin == null)
                throw new ArgumentNullException("Note skin cannot be null.");
            else if (keyWidth <= 0)
                throw new ArgumentOutOfRangeException("Key width must be greater than zero.");
            else if (pixelsPerSecond <= 0)
                throw new ArgumentOutOfRangeException("Pixels per second must be greater than zero.");

            KeyWidth = keyWidth;

            Chart = chart;
            NoteSkin = noteSkin;
            NoteSkin.ScaleToNoteFieldKeyWidth(KeyWidth);

            // 4 measures
            ExtraEndBeats = 16;
            PixelsPerSecond = pixelsPerSecond;
            ScrollScalar = 0.25;
            TopTimeMargin = timeOffset;

            ChartEvents = new ChartEventBus(Chart);
            ScrollBottom = new ScrollState(this);
            ScrollTop = new ScrollState(this);

            ScrollTop.positionOffset = (int)Math.Round(TopTimeMargin.Value * PixelsPerSecond);
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
            var newStepsScrolled = stepsScrolled + (scrollDelta * ScrollScalar);

            if (newStepsScrolled < 0)
                newStepsScrolled = 0;
            else
            {
                var maxScroll = (ChartLength.Value - TopTimeMargin.Value) * stepsPerSecond;

                if (newStepsScrolled > maxScroll)
                    newStepsScrolled = maxScroll;
            }

            // Nothing changed.
            if (newStepsScrolled == stepsScrolled && viewportHeight == oldViewportHeight)
                return false;

            oldViewportHeight = viewportHeight;
            stepsScrolled = newStepsScrolled;

            var absTime = (stepsScrolled * stepsPerSecond) - TopTimeMargin.Value;

            if (absTime > ChartLength.Value)
                absTime = ChartLength.Value;

            if (absTime < 0)
                ScrollTop.positionOffset = (int)Math.Round(Math.Abs(absTime) * PixelsPerSecond);
            else
                ScrollTop.positionOffset = 0;

            ScrollTop.Time = Math.Max(absTime, 0);
            ScrollTop.IntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollTop.Time);
            ScrollTop.Beat = Chart.BPMList.Time.TimeToBeat(ScrollTop.Time, ScrollTop.IntervalIndex);

            ScrollBottom.Time = absTime + Math.Floor((double)viewportHeight / PixelsPerSecond);
            ScrollBottom.IntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollBottom.Time);
            ScrollBottom.Beat = Chart.BPMList.Time.TimeToBeat(ScrollBottom.Time, ScrollBottom.IntervalIndex);

            return true;
        }
    }
}
