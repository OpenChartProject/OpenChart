namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// Note field widget class that contains the key widgets for a chart.
    /// </summary>
    public class KeyContainer : IWidget
    {
        public Key[] Keys { get; private set; }
        public Gtk.Widget GetWidget() => null;
    }
}
