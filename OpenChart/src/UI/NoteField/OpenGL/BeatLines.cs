using System;
using System.Collections.Generic;

namespace OpenChart.UI.NoteField.OpenGL
{
    public class BeatLines : IDrawable
    {
        /// <summary>
        /// The settings for how the beat lines are displayed.
        /// </summary>
        public BeatLineSettings BeatLineSettings { get; private set; }

        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        public BeatLines(NoteFieldSettings noteFieldSettings, BeatLineSettings beatLineSettings)
        {
            BeatLineSettings = beatLineSettings;
            NoteFieldSettings = noteFieldSettings;
        }

        public void Draw(Cairo.Context ctx)
        {
            // Get the area of the widget we are redrawing.
            var clip = ctx.ClipExtents();

            // Based on what's displayed on screen, get the time in the chart at the top of the
            // widget for the beat lines. This is to prevent drawing beat lines that aren't visible.
            // We don't need to worry about negative values here since clip.Y is never negative.
            var topTime = clip.Y / NoteFieldSettings.PixelsPerSecond;
            var bottomTime = (clip.Y + clip.Height) / NoteFieldSettings.PixelsPerSecond;

            var beatLines = new List<int>();
            var measureLines = new List<int>();

            // Iterate through the beats in the chart and record the y positions of each line.
            foreach (var beat in NoteFieldSettings.Chart.BPMList.Time.GetBeats(topTime))
            {
                if (beat.Time.Value >= bottomTime)
                    break;

                var y = (int)Math.Round(beat.Time.Value * NoteFieldSettings.PixelsPerSecond);

                if (beat.Beat.IsStartOfMeasure())
                    measureLines.Add(y);
                else
                    beatLines.Add(y);
            }

            // Draw the beat lines that occur at the start of a measure.
            drawBeatLines(
                ctx,
                (int)clip.Width,
                BeatLineSettings.MeasureLineColor,
                BeatLineSettings.MeasureLineThickness,
                measureLines
            );

            // Draw all of the other beat lines.
            drawBeatLines(
                ctx,
                (int)clip.Width,
                BeatLineSettings.BeatLineColor,
                BeatLineSettings.BeatLineThickness,
                beatLines
            );
        }

        /// <summary>
        /// Draws horizontal beat lines at the given positions.
        /// </summary>
        private void drawBeatLines(Cairo.Context cr, int width, Color color, int thickness, List<int> positions)
        {
            cr.SetSourceColor(color.AsCairoColor());
            cr.LineWidth = thickness;

            // Fix the line not being the right thickness if the thickness is an odd number.
            var offset = (thickness % 2 == 1) ? 0.5 : 0;

            foreach (var y in positions)
            {
                var offsetY = y + offset;

                cr.MoveTo(0, offsetY);
                cr.LineTo(width, offsetY);
                cr.Stroke();
            }
        }
    }
}
