namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// The complete representation of a note field. This class includes all of the necessary
    /// components for a note field.
    /// </summary>
    public class NoteFieldView : IWidget
    {
        public BeatLines BeatLines { get; private set; }
        public KeyContainer Keys { get; private set; }

        public NoteFieldSettings NoteFieldSettings { get; private set; }

        public Gtk.Widget GetWidget() => null;
    }
}
