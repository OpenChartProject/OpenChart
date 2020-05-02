using Gtk;

namespace OpenChart.UI.Widgets
{
    public class BeatLines : DrawingArea, INoteFieldWidget
    {
        public NoteField NoteField { get; set; }

        protected override bool OnDrawn(Cairo.Context cr)
        {
            return true;
        }
    }
}
