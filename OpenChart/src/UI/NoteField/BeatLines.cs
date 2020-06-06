using Gtk;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// The display settings for the BeatLines widget.
    /// </summary>
    public class BeatLineSettings
    {
        /// <summary>
        /// The color of a beat line.
        /// </summary>
        public Color BeatLineColor { get; set; }

        /// <summary>
        /// The thickness (in pixels) of a beat line.
        /// </summary>
        public int BeatLineThickness { get; set; }

        /// <summary>
        /// The color for the start of a measure.
        /// </summary>
        public Color MeasureLineColor { get; set; }

        /// <summary>
        /// The thickness (in pixels) for the start of a measure.
        /// </summary>
        public int MeasureLineThickness { get; set; }
    }

    /// <summary>
    /// Note field widget for displaying beat lines.
    /// </summary>
    public class BeatLines : IWidget
    {
        public BeatLineSettings BeatLineSettings { get; private set; }
        public DisplaySettings DisplaySettings { get; private set; }

        DrawingArea drawingArea;
        public Widget GetWidget() => drawingArea;

        public BeatLines(DisplaySettings displaySettings, BeatLineSettings beatLineSettings)
        {
            BeatLineSettings = beatLineSettings;
            DisplaySettings = displaySettings;

            drawingArea = new DrawingArea();
            drawingArea.Drawn += onDrawn;
        }

        private void onDrawn(object o, DrawnArgs e)
        {

        }
    }
}
