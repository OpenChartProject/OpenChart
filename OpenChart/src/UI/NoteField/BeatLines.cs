using System.Collections.Generic;

namespace OpenChart.UI.NoteField
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

        public void Draw(DrawingContext ctx)
        {
            var beatLines = new List<int>();
            var measureLines = new List<int>();

            // Iterate through the beats in the chart and record the y positions of each line.
            foreach (var beat in NoteFieldSettings.Chart.BPMList.Time.GetBeats(ctx.Top.Time))
            {
                if (beat.Time.Value >= ctx.Bottom.Time.Value)
                    break;

                var y = NoteFieldSettings.TimeToPosition(beat.Time);

                if (beat.Beat.IsStartOfMeasure())
                    measureLines.Add(y);
                else
                    beatLines.Add(y);
            }

            // Draw the beat lines that occur at the start of a measure.
            drawBeatLines(
                ctx.Cairo,
                NoteFieldSettings.NoteFieldWidth,
                BeatLineSettings.MeasureLineColor,
                BeatLineSettings.MeasureLineThickness,
                measureLines
            );

            // Draw all of the other beat lines.
            drawBeatLines(
                ctx.Cairo,
                NoteFieldSettings.NoteFieldWidth,
                BeatLineSettings.BeatLineColor,
                BeatLineSettings.BeatLineThickness,
                beatLines
            );
        }

        /// <summary>
        /// Draws horizontal beat lines at the given positions.
        /// </summary>
        private void drawBeatLines(Cairo.Context cr, int width, Cairo.Color color, int thickness, List<int> positions)
        {
            cr.SetSourceColor(color);
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
