using Gdk;
using Gtk;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// Note field widget for drawing beat lines.
    /// </summary>
    public class BeatLines : DrawingArea
    {
        readonly int lineThickness = 1;
        readonly RGBA lineColor = new RGBA
        {
            Red = 0.5,
            Green = 0.5,
            Blue = 0.5,
            Alpha = 1.0
        };

        readonly int measureLineThickness = 2;
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
        public readonly NoteField NoteField;

        /// <summary>
        /// Creates a new BeatLines instance.
        /// </summary>
        public BeatLines(NoteField noteField)
        {
            NoteField = noteField;
        }

        /// <summary>
        /// Draws lines at each beat. The line for the first beat of each measure is drawn
        /// thicker and brighter than lines for beats that occur in the middle of a measure.
        /// </summary>
        protected override bool OnDrawn(Cairo.Context cr)
        {
            var index = NoteField.ScrollIntervalIndex;
            var time = NoteField.ScrollTime;
            var iterator = NoteField.Chart.BPMList.Time.GetBeats(time, index);
            var beat = NoteField.ScrollBeat;

            // The y-position of beat lines that mark the start of a measure. We're assuming
            // 4/4 time so this will be every 4th beat.
            var startOfMeasure = new List<int>();

            // The y-position of beat lines that occur during a measure.
            var duringMeasure = new List<int>();

            foreach (var beatTime in iterator)
            {
                var y = NoteField.GetYPosOfTime(beatTime.Time);

                if (y > NoteField.ViewportBottomY)
                    break;

                if (beatTime.Beat.IsStartOfMeasure())
                    startOfMeasure.Add(y);
                else
                    duringMeasure.Add(y);
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
            cr.SetSourceRGBA(color.Red, color.Green, color.Blue, color.Alpha);

            foreach (var y in yPositions)
            {
                cr.MoveTo(0, y + offset);
                cr.LineTo(AllocatedWidth, y + offset);
            }

            cr.Stroke();
        }
    }
}
