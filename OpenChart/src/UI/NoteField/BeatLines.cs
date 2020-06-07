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

            drawingArea.SetSizeRequest(400, 2000);

            drawingArea.SizeAllocated += (o, e) =>
            {
                Serilog.Log.Debug($"Drawing area allocated: {e.Allocation.Width}x{e.Allocation.Height}");
            };
        }

        private void onDrawn(object o, DrawnArgs e)
        {
            var clip = e.Cr.ClipExtents();

            // The time in the chart that the top of the drawing area is.
            var topTime = clip.Y * DisplaySettings.PixelsPerSecond;
            var bottomTime = (clip.Y + clip.Height) * DisplaySettings.PixelsPerSecond;

            foreach (var beat in DisplaySettings.Chart.BPMList.Time.GetBeats(topTime))
            {
                // Done drawing.
                if (beat.Time.Value > bottomTime)
                    break;

                var y = beat.Time.Value / DisplaySettings.PixelsPerSecond;

                e.Cr.SetSourceColor(BeatLineSettings.BeatLineColor.AsCairoColor());
                e.Cr.LineWidth = BeatLineSettings.BeatLineThickness;

                e.Cr.MoveTo(0, y);
                e.Cr.LineTo(clip.Width, y);
                e.Cr.Stroke();
            }
        }
    }
}
