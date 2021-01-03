using OpenChart.Charting;
using OpenChart.Charting.Properties;
using OpenChart.NoteSkins;
using Serilog;
using System;

namespace OpenChart.UI.Components.NoteField
{
    /// <summary>
    /// Note field settings for modifying how the note field looks.
    /// </summary>
    public class NoteFieldSettings
    {
        /// <summary>
        /// The alignment for note field objects.
        /// </summary>
        public NoteFieldObjectAlignment Alignment { get; private set; }

        /// <summary>
        /// The amount to offset chart objects by. This is affected by the Alignment. To use this,
        /// multiply the base line by the object's height, then reposition the object using that
        /// value as an offset.
        /// </summary>
        public double BaseLine => getBaseLine();

        /// <summary>
        /// The chart the note field is displaying.
        /// </summary>
        public Chart Chart { get; private set; }

        /// <summary>
        /// An event bus for the chart.
        /// </summary>
        public ChartEventBus ChartEventBus { get; private set; }

        /// <summary>
        /// The number of extra measures to append to the end of the chart.
        /// </summary>
        public int ExtraMeasures = 4;

        /// <summary>
        /// The width, in pixels, of each key in the note field.
        /// </summary>
        public int KeyWidth { get; private set; }

        /// <summary>
        /// The width, in pixels, of the entire note field.
        /// </summary>
        public int NoteFieldWidth => Chart.KeyCount.Value * KeyWidth;

        /// <summary>
        /// The note skin to use for the note field.
        /// </summary>
        public KeyModeSkin NoteSkin { get; private set; }

        /// <summary>
        /// The beattime at the bottom of the notefield container. Note that this is not the beattime
        /// for the end of the chart. As the user scrolls this value will change based on which
        /// part of the chart is visible.
        /// </summary>
        public BeatTime Bottom { get; set; }

        /// <summary>
        /// The beattime at the top of the notefield container. Note that this is not the beattime
        /// for the beginning of the chart. As the user scrolls this value will change based on which
        /// part of the chart is visible.
        /// </summary>
        public BeatTime Top { get; set; }

        /// <summary>
        /// The y-pos of the receptors.
        /// </summary>
        public int ReceptorY => Y;

        public BeatTime ReceptorBeatTime { get; private set; }

        public BeatDivision BeatSnap { get; private set; }

        /// <summary>
        /// The number of pixels that represents one second of time in the chart. This value is
        /// affected by <see cref="Zoom" />.
        /// </summary>
        public int PixelsPerSecond { get; private set; }
        public int ScaledPixelsPerSecond => (int)Math.Round(PixelsPerSecond * Zoom);

        public int Y { get; private set; }
        public float Zoom { get; private set; }

        /// <summary>
        /// Creates a new NoteFieldSettings instance.
        /// </summary>
        /// <param name="chart">The chart to display.</param>
        /// <param name="noteSkin">The note skin for the note field.</param>
        /// <param name="pixelsPerSecond">The time to pixel ratio.</param>
        /// <param name="keyWidth">The width, in pixels, of a single key.</param>
        public NoteFieldSettings(
            Chart chart,
            KeyModeSkin noteSkin,
            int pixelsPerSecond,
            int keyWidth,
            NoteFieldObjectAlignment alignment
        )
        {
            Alignment = alignment;
            Chart = chart;
            NoteSkin = noteSkin;
            KeyWidth = keyWidth;
            PixelsPerSecond = pixelsPerSecond;
            BeatSnap = 1;
            Y = 0;
            Zoom = 1.0f;
            ReceptorBeatTime = new BeatTime(0, 0);

            NoteSkin.ScaleToNoteFieldKeyWidth(KeyWidth);

            ChartEventBus = new ChartEventBus(Chart);
        }

        /// <summary>
        /// Returns the position of the given beat.
        /// </summary>
        public int BeatToPosition(Beat beat)
        {
            return TimeToPosition(Chart.BPMList.Time.BeatToTime(beat));
        }

        /// <summary>
        /// Returns the position of the given time.
        /// </summary>
        public int TimeToPosition(Time time)
        {
            return (int)Math.Round(time.Value * ScaledPixelsPerSecond) - Y;
        }

        /// <summary>
        /// Scrolls the notefield.
        /// </summary>
        public void Scroll(double delta)
        {
            Beat beat;

            if (delta > 0)
                beat = BeatSnap.NextDivisionFromBeat(ReceptorBeatTime.Beat);
            else
                beat = BeatSnap.PrevDivisionFromBeat(ReceptorBeatTime.Beat);

            ScrollTo(BeatToPosition(beat) + Y);
        }

        /// <summary>
        /// Scrolls the notefield to a specific position.
        /// </summary>
        public void ScrollTo(int y)
        {
            Y = Math.Max(y, 0);

            var time = (double)Y / ScaledPixelsPerSecond;
            var beat = Chart.BPMList.Time.TimeToBeat(time);

            ReceptorBeatTime.Beat = beat;
            ReceptorBeatTime.Time = time;
        }

        private double getBaseLine()
        {
            switch (Alignment)
            {
                case NoteFieldObjectAlignment.Bottom:
                    return 0;
                case NoteFieldObjectAlignment.Center:
                    return 0.5;
                case NoteFieldObjectAlignment.Top:
                    return 1.0;
            }

            return 0;
        }
    }
}
