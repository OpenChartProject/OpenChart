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
        /// The height of the chart, in pixels.
        /// </summary>
        public int ChartHeight => (int)Math.Ceiling(Chart.GetTimeLength().Value * PixelsPerSecond);

        /// <summary>
        /// The number of pixels that represent a full second. This is used to calculate where to
        /// draw things like beat lines and notes.
        /// </summary>
        public int PixelsPerSecond { get; private set; }

        /// <summary>
        /// The width, in pixels, of each key in the note field.
        /// </summary>
        public int KeyWidth { get; private set; }

        /// <summary>
        /// The width, in pixels, of the entire note field.
        /// </summary>
        public int NoteFieldWidth => Chart.KeyCount.Value * KeyWidth;

        public DisplaySettings(Chart chart, int pixelsPerSecond, int keyWidth)
        {
            Chart = chart;
            PixelsPerSecond = pixelsPerSecond;
            KeyWidth = keyWidth;
        }
    }
}
