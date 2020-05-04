using Gtk;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    public class BeatLines : DrawingArea, IHasNoteField
    {
        public NoteField NoteField { get; set; }

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

            cr.SetSourceRGB(1, 1, 1);
            cr.LineWidth = 3;

            // Draw the measure start beat lines.
            foreach (var y in startOfMeasure)
            {
                cr.MoveTo(0, y + 0.5);
                cr.LineTo(AllocatedWidth, y + 0.5);
            }

            cr.Stroke();

            cr.SetSourceRGB(0.65, 0.65, 0.65);
            cr.LineWidth = 1;

            // Draw the beat lines for during a measure.
            foreach (var y in duringMeasure)
            {
                cr.MoveTo(0, y + 0.5);
                cr.LineTo(AllocatedWidth, y + 0.5);
            }

            cr.Stroke();

            return true;
        }
    }
}
