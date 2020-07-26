using OpenChart.Charting;
using OpenChart.Charting.Properties;
using OpenChart.NoteSkins;
using OpenChart.UI.NoteField.Objects;
using System;

namespace OpenChart.UI.NoteField
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
        /// The height of the note field, in pixels. This is the total height of the chart plus
        /// the extra end measures.
        /// </summary>
        public int NoteFieldHeight
        {
            get
            {
                var measure = Math.Ceiling(Chart.GetBeatLength().Value / 4) + ExtraMeasures;
                var beat = measure * 4;

                return BeatToPosition(beat);
            }
        }

        /// <summary>
        /// The width, in pixels, of the entire note field.
        /// </summary>
        public int NoteFieldWidth => Chart.KeyCount.Value * KeyWidth;

        /// <summary>
        /// The note skin to use for the note field.
        /// </summary>
        public KeyModeSkin NoteSkin { get; private set; }

        /// <summary>
        /// The object factory for creating new note field objects.
        /// </summary>
        public NoteFieldObjectFactory ObjectFactory { get; private set; }

        /// <summary>
        /// The number of pixels that represent a full second. This is used to calculate where to
        /// draw things like beat lines and notes.
        /// </summary>
        public int PixelsPerSecond { get; private set; }

        Time _receptorPosition;

        /// <summary>
        /// The position of the receptors, in seconds. This changes as the user scrolls through the
        /// chart or while the song is playing.
        /// </summary>
        public Time ReceptorTime
        {
            get => _receptorPosition;
            set
            {
                _receptorPosition = value;
                ReceptorPositionChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// An event fired when the receptor position is changed.
        /// </summary>
        public event EventHandler ReceptorPositionChanged;

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
            PixelsPerSecond = pixelsPerSecond;
            KeyWidth = keyWidth;

            NoteSkin.ScaleToNoteFieldKeyWidth(KeyWidth);

            ChartEventBus = new ChartEventBus(Chart);
            ObjectFactory = new NoteFieldObjectFactory(this);
        }

        /// <summary>
        /// Returns the vertical offset for a widget given the current alignment settings.
        /// </summary>
        /// <param name="widgetHeight">The height of the widget, in pixels.</param>
        public int AlignmentOffset(int widgetHeight)
        {
            if (Alignment == NoteFieldObjectAlignment.Top)
                return 0;
            else if (Alignment == NoteFieldObjectAlignment.Center)
                return -widgetHeight / 2;
            else if (Alignment == NoteFieldObjectAlignment.Bottom)
                return -widgetHeight;

            throw new Exception("Unknown notefield object alignment type.");
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
            return (int)Math.Round(time.Value * PixelsPerSecond);
        }
    }
}
