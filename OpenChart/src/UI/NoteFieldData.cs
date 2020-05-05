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
            /// <value></value>
            public int Position { get; internal set; }

            /// <summary>
            /// The scroll position (in seconds).
            /// </summary>
            public Time Time { get; internal set; }

            /// <summary>
            /// Creates a new ScrollState instance.
            /// </summary>
            public ScrollState()
            {
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
        /// The amount the note field is scrolled by for one scroll step (in pixels). A scroll step is
        /// one "click" on a mouse wheel.
        /// </summary>
        public int ScrollStepSize { get; internal set; }

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

            PixelsPerSecond = pixelsPerSecond;
            Chart = chart;
            NoteSkin = noteSkin;

            ChartEvents = new ChartEventBus(Chart);
            ScrollBottom = new ScrollState();
            ScrollTop = new ScrollState();
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
        /// </summary>
        /// <param name="stepPosition">The number of scroll steps the widget is scrolled.</param>
        /// <param name="viewPortHeight">The height of the widget (in pixels).</param>
        public void UpdateScroll(double stepPosition, int viewPortHeight)
        {

        }
    }
}
