using OpenChart.Charting;
using System;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// Note field settings for modifying how the note field looks.
    /// </summary>
    public class DisplaySettings
    {
        /// <summary>
        /// The chart the note field is displaying.
        /// </summary>
        public Chart Chart { get; private set; }

        /// <summary>
        /// An event bus for the chart.
        /// </summary>
        public ChartEventBus ChartEventBus { get; private set; }

        /// <summary>
        /// The number of extra beats to append to the end of the chart.
        /// </summary>
        public int ExtraMeasures = 4;

        /// <summary>
        /// The number of pixels that represent a full second. This is used to calculate where to
        /// draw things like beat lines and notes.
        /// </summary>
        public int PixelsPerSecond { get; private set; }

        /// <summary>
        /// The width, in pixels, of each key in the note field.
        /// </summary>
        public int KeyWidth { get; private set; }

        public int NoteFieldHeight
        {
            get
            {
                var measure = Math.Ceiling(Chart.GetBeatLength().Value / 4) + ExtraMeasures;
                var beat = measure * 4;

                return (int)Math.Ceiling(Chart.BPMList.Time.BeatToTime(beat).Value * PixelsPerSecond);
            }
        }

        /// <summary>
        /// The width, in pixels, of the entire note field.
        /// </summary>
        public int NoteFieldWidth => Chart.KeyCount.Value * KeyWidth;

        public DisplaySettings(Chart chart, int pixelsPerSecond, int keyWidth)
        {
            Chart = chart;
            ChartEventBus = new ChartEventBus(Chart);
            PixelsPerSecond = pixelsPerSecond;
            KeyWidth = keyWidth;
        }
    }
}
