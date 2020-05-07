using Gdk;
using Gtk;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// Note field widget for drawing beat lines. Beat lines are drawn for beats which are
    /// whole numbers (1, 2, 3, etc.). Beat lines which mark the start of a measure are
    /// drawn bold.
    /// </summary>
    public class BeatLines : DrawingArea
    {
        const int MaxBeatLinesDrawn = 500;

        /// <summary>
        /// The line thickness (in pixels) for beat lines which occur during a measure.
        /// </summary>
        readonly int lineThickness = 1;

        /// <summary>
        /// The line color for beat lines which occur during a measure.
        /// </summary>
        readonly RGBA lineColor = new RGBA
        {
            Red = 0.5,
            Green = 0.5,
            Blue = 0.5,
            Alpha = 1.0
        };

        /// <summary>
        /// The line thickness (in pixels) for beat lines which occur at the start of a measure.
        /// </summary>
        readonly int measureLineThickness = 2;

        /// <summary>
        /// The line color for beat lines which occur at the start of a measure.
        /// </summary>
        readonly RGBA measureLineColor = new RGBA
        {
            Red = 0.7,
            Green = 0.7,
            Blue = 0.7,
            Alpha = 1.0
        };

        /// <summary>
        /// The note field this widget is for.
        /// </summary>
        readonly NoteFieldData noteFieldData;

        /// <summary>
        /// Creates a new BeatLines instance.
        /// </summary>
        public BeatLines(NoteFieldData noteFieldData)
        {
            this.noteFieldData = noteFieldData;
        }

        /// <summary>
        /// Draws lines at each beat. The line for the first beat of each measure is drawn
        /// thicker and brighter than lines for beats that occur in the middle of a measure.
        /// </summary>
        protected override bool OnDrawn(Cairo.Context cr)
        {
            // Get the beats that are visible on the screen.
            var iterator = noteFieldData.Chart.BPMList.Time.GetBeats(
                noteFieldData.ScrollTop.Time,
                noteFieldData.ScrollTop.IntervalIndex
            );

            // The y-position of beat lines that mark the start of a measure. We're assuming
            // 4/4 time so this will be every 4th beat.
            var startOfMeasure = new List<int>();

            // The y-position of beat lines that occur during a measure.
            var duringMeasure = new List<int>();

            var lineCount = 0;

            foreach (var beatTime in iterator)
            {
                // Beat line is off screen, we're done.
                if (beatTime.Time.Value > noteFieldData.ScrollBottom.Time.Value)
                    break;
                else if (lineCount >= MaxBeatLinesDrawn)
                    break;

                var y = noteFieldData.GetPosition(beatTime.Time);

                if (beatTime.Beat.IsStartOfMeasure())
                    startOfMeasure.Add(y);
                else
                    duringMeasure.Add(y);

                lineCount++;
            }

            drawBeatLines(cr, startOfMeasure.ToArray(), measureLineThickness, measureLineColor);
            drawBeatLines(cr, duringMeasure.ToArray(), lineThickness, lineColor);

            return true;
        }

        /// <summary>
        /// Draws a line at each y-position using the provided thickness and color.
        /// </summary>
        private void drawBeatLines(Cairo.Context cr, int[] yPositions, int thickness, RGBA color)
        {
            var offset = thickness % 2 == 0 ? 0 : 0.5;

            cr.LineWidth = thickness;
            Gdk.CairoHelper.SetSourceRgba(cr, color);

            foreach (var y in yPositions)
            {
                cr.MoveTo(0, y + offset);
                cr.LineTo(AllocatedWidth, y + offset);
            }

            cr.Stroke();
        }
    }
}
