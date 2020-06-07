using Gtk;
using System;
using System.Collections.Generic;

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
        /// <summary>
        /// The drawing settings to use for the beat lines.
        /// </summary>
        public BeatLineSettings BeatLineSettings { get; private set; }

        /// <summary>
        /// The display settings for the note field the beat lines are for.
        /// </summary>
        public DisplaySettings DisplaySettings { get; private set; }

        DrawingArea drawingArea;
        public Widget GetWidget() => drawingArea;

        /// <summary>
        /// Creates a new BeatLines instance.
        /// </summary>
        /// <param name="displaySettings">The settings for the note field.</param>
        /// <param name="beatLineSettings">The settings for the BeatLines instance.</param>
        public BeatLines(DisplaySettings displaySettings, BeatLineSettings beatLineSettings)
        {
            BeatLineSettings = beatLineSettings;
            DisplaySettings = displaySettings;

            drawingArea = new DrawingArea();
            drawingArea.Drawn += onDrawn;

            // Resize the widget when the note field is updated.
            DisplaySettings.ChartEventBus.Anything += delegate { resizeToFit(); };
            resizeToFit();
        }

        /// <summary>
        /// Draws horizontal beat lines at the given positions.
        /// </summary>
        private void drawBeatLines(Cairo.Context cr, int width, Color color, int thickness, List<int> positions)
        {
            cr.SetSourceColor(color.AsCairoColor());
            cr.LineWidth = thickness;

            // Fix the line not being the right thickness if the thickness is an odd number.
            var applyOffset = (thickness % 2 == 1);

            foreach (var y in positions)
            {
                var offsetY = y + (applyOffset ? 0.5 : 0);

                cr.MoveTo(0, offsetY);
                cr.LineTo(width, offsetY);
                cr.Stroke();
            }
        }

        private void onDrawn(object o, DrawnArgs e)
        {
            // Get the area of the widget we are redrawing.
            var clip = e.Cr.ClipExtents();

            // The time in the chart that the top of the drawing area is.
            var topTime = clip.Y / DisplaySettings.PixelsPerSecond;
            var bottomTime = (clip.Y + clip.Height) / DisplaySettings.PixelsPerSecond;

            var beatLines = new List<int>();
            var measureLines = new List<int>();

            // Iterate through the beats in the chart and record the y positions of each line
            foreach (var beat in DisplaySettings.Chart.BPMList.Time.GetBeats(topTime))
            {
                if (beat.Time.Value >= bottomTime)
                    break;

                var y = (int)Math.Round(beat.Time.Value * DisplaySettings.PixelsPerSecond);

                if (beat.Beat.IsStartOfMeasure())
                    measureLines.Add(y);
                else
                    beatLines.Add(y);
            }

            // Draw the beat lines that occur at the start of a measure.
            drawBeatLines(
                e.Cr,
                (int)clip.Width,
                BeatLineSettings.MeasureLineColor,
                BeatLineSettings.MeasureLineThickness,
                measureLines
            );

            // Draw all of the other beat lines.
            drawBeatLines(
                e.Cr,
                (int)clip.Width,
                BeatLineSettings.BeatLineColor,
                BeatLineSettings.BeatLineThickness,
                beatLines
            );
        }

        /// <summary>
        /// Resizes the widget to fit the size of the note field.
        /// </summary>
        private void resizeToFit()
        {
            drawingArea.SetSizeRequest(DisplaySettings.NoteFieldWidth, DisplaySettings.NoteFieldHeight);
        }
    }
}
