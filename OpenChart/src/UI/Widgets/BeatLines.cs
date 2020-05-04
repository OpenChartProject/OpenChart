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
            cr.SetSourceRGBA(1, 1, 1, 1);

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

                if (beatTime.Beat.Value % 4 == 0)
                    startOfMeasure.Add(y);
                else
                    duringMeasure.Add(y);
            }

            double offset;

            cr.LineWidth = measureLineThickness;
            cr.SetSourceRGBA(
                measureLineColor.Red,
                measureLineColor.Green,
                measureLineColor.Blue,
                measureLineColor.Alpha
            );

            // Cairo uses pen drawing so we need to realign when the line width is an even number.
            offset = cr.LineWidth % 2 == 0 ? 0.5 : 0;

            // Draw the measure start beat lines.
            foreach (var y in startOfMeasure)
            {
                cr.MoveTo(0, y + offset);
                cr.LineTo(AllocatedWidth, y + offset);
            }

            cr.Stroke();

            cr.LineWidth = lineThickness;
            cr.SetSourceRGBA(
                lineColor.Red,
                lineColor.Green,
                lineColor.Blue,
                lineColor.Alpha
            );

            offset = cr.LineWidth % 2 == 0 ? 0 : 0.5;

            // Draw the beat lines for during a measure.
            foreach (var y in duringMeasure)
            {
                cr.MoveTo(0, y + offset);
                cr.LineTo(AllocatedWidth, y + offset);
            }

            cr.Stroke();

            return true;
        }
    }
}
