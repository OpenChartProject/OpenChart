using Serilog;
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
            var beatLines = new List<int>();
            var measureLines = new List<int>();

            var clip = ctx.ClipExtents();
            var pps = (double)NoteFieldSettings.ScaledPixelsPerSecond;
            var topTime = (NoteFieldSettings.Y / pps);
            var bottomTime = (NoteFieldSettings.Y + clip.Height) / pps;

            if (topTime < 0)
                topTime = 0;

            if (bottomTime < 0)
                bottomTime = 0;

            // Iterate through the beats in the chart and record the y positions of each line.
            foreach (var beat in NoteFieldSettings.Chart.BPMList.Time.GetBeats(topTime))
            {
                if (beat.Time.Value >= bottomTime)
                    break;

                var y = (int)Math.Round(beat.Time.Value * pps) - NoteFieldSettings.Y;

                if (beat.Beat.IsStartOfMeasure())
                    measureLines.Add(y);
                else
                    beatLines.Add(y);
            }

            // Draw the beat lines that occur at the start of a measure.
            drawBeatLines(
                ctx,
                NoteFieldSettings.NoteFieldWidth,
                BeatLineSettings.MeasureLineColor,
                BeatLineSettings.MeasureLineThickness,
                measureLines
            );

            // Draw all of the other beat lines.
            drawBeatLines(
                ctx,
                NoteFieldSettings.NoteFieldWidth,
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
