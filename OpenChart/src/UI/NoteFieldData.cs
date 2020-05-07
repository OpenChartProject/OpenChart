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
            /// The scroll position (in pixels) relative to the first beat of the chart.
            /// This does not take into account any note field offsets.
            /// </summary>
            public int Position => (int)Math.Round(Time.Value * data.PixelsPerSecond);

            /// <summary>
            /// The scroll position, including the offset (in pixels). Used by the note field
            /// to offset widgets. This value can be negative.
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

        /// <summary>
        /// How much time 1 scroll step represents.
        /// </summary>
        int stepsPerSecond = 1;

        /// <summary>
        /// When set to true, objects are offset so that the center of the object is considered
        /// the origin. When false, the origin is considered the top of the object.
        /// </summary>
        public bool CenterObjectsOnBeatLines { get; private set; }

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
        /// The scroll position of the bottom of the viewport.
        ///
        /// <seealso cref="UpdateScroll" />
        /// </summary>
        public ScrollState ScrollBottom { get; private set; }

        /// <summary>
        /// The scroll position of the top of the viewport.
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
        /// <param name="centerObjectsOnBeatLines">Centers note objects along beat lines.</param>
        public NoteFieldData(
            Chart chart,
            KeyModeSkin noteSkin,
            int keyWidth,
            int pixelsPerSecond,
            Time timeOffset,
            bool centerObjectsOnBeatLines
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
            CenterObjectsOnBeatLines = centerObjectsOnBeatLines;
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

            // Initialize the scroll positions.
            updateScrollTop();
            updateScrollBottom(0);
        }

        /// <summary>
        /// Gets the absolute time of the current scroll position. The absolute time has no bounds,
        /// and takes into account any note field margins. Hence, the absolute time can be negative.
        /// </summary>
        public double GetAbsoluteTime()
        {
            return GetAbsoluteTime(stepsScrolled);
        }

        /// <summary>
        /// Gets the absolute time at the given step count.
        /// </summary>
        /// <param name="steps">The number of scroll steps.</param>
        public double GetAbsoluteTime(double steps)
        {
            return (steps * stepsPerSecond) - TopTimeMargin.Value;
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
        /// Updates the scroll positions in response to the viewport being resized.
        /// </summary>
        /// <param name="height">The height of the viewport (in pixels).</param>
        public void OnViewportResize(int height)
        {
            updateScrollBottom(height);
        }

        /// <summary>
        /// Updates the scroll positions in response to the user scrolling the note field.
        ///
        /// Returns true if the scroll position changed due to the scroll input.
        /// </summary>
        /// <param name="delta">The scroll steps.</param>
        /// <param name="height">The height of the viewport (in pixels).</param>
        public bool OnScroll(double delta, int viewportHeight)
        {
            var maxScroll = (ChartLength.Value - TopTimeMargin.Value) * stepsPerSecond;
            var newStepsScrolled = stepsScrolled + (delta * ScrollScalar);

            newStepsScrolled = Math.Clamp(newStepsScrolled, 0, maxScroll);

            // Nothing to update.
            if (newStepsScrolled == stepsScrolled)
                return false;

            stepsScrolled = newStepsScrolled;

            updateScrollBottom(viewportHeight);
            updateScrollTop();

            return true;
        }

        /// <summary>
        /// Updates ScrollTop using the current scroll position.
        /// </summary>
        private void updateScrollTop()
        {
            var absTime = GetAbsoluteTime();

            if (absTime < 0)
                ScrollTop.positionOffset = (int)Math.Abs(Math.Round(absTime * PixelsPerSecond));
            else
                ScrollTop.positionOffset = 0;

            ScrollTop.Time = Math.Max(0, absTime);
            ScrollTop.IntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollTop.Time);
            ScrollTop.Beat = Chart.BPMList.Time.TimeToBeat(ScrollTop.Time, ScrollTop.IntervalIndex);
        }

        /// <summary>
        /// Updates ScrollBottom using the current scroll position.
        /// </summary>
        private void updateScrollBottom(int viewportHeight)
        {
            var absTime = GetAbsoluteTime();
            var heightToTime = (double)viewportHeight / PixelsPerSecond;

            ScrollBottom.Time = Math.Max(0, absTime + heightToTime);
            ScrollBottom.IntervalIndex = Chart.BPMList.Time.GetIndexAtTime(ScrollBottom.Time);
            ScrollBottom.Beat = Chart.BPMList.Time.TimeToBeat(ScrollBottom.Time, ScrollBottom.IntervalIndex);
        }
    }
}
