namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// Note field widget for displaying beat lines.
    /// </summary>
    public class BeatLines : IWidget
    {
        public DisplaySettings DisplaySettings { get; private set; }

        Gtk.DrawingArea drawingArea;
        public Gtk.Widget GetWidget() => null;

        public BeatLines(DisplaySettings settings)
        {
            drawingArea = new Gtk.DrawingArea();
            DisplaySettings = settings;
        }
    }
}
