using Gtk;
using System;

namespace OpenChart.UI.Widgets
{
    public class BeatLines : DrawingArea, INoteFieldWidget
    {
        public NoteField NoteField { get; set; }

        protected override bool OnDrawn(Cairo.Context cr)
        {
            cr.SetSourceRGBA(1, 1, 1, 1);
            cr.LineWidth = 1;

            var index = NoteField.ScrollIntervalIndex;
            var time = NoteField.ScrollTime;
            var iterator = NoteField.Chart.BPMList.Time.GetTimeOfNextBeat(time, index);

            foreach (var nextTime in iterator)
            {
                var y = NoteField.GetYPosOfTime(nextTime.Value);

                if (y > NoteField.ViewportBottomY)
                    break;

                cr.MoveTo(0, y + 0.5);
                cr.LineTo(AllocatedWidth, y + 0.5);
            }

            cr.Stroke();

            return true;
        }
    }
}
