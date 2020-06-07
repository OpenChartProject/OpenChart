using OpenChart.Charting.Properties;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// Note field widget class that represents a single key on the note field. The widget is
    /// a simple fixed container that renders the objects for a particular key in the chart.
    /// </summary>
    public class Key : IWidget
    {
        public NoteFieldSettings NoteFieldSettings { get; private set; }
        /// <summary>
        /// The key index in the chart that this widget represents.
        /// </summary>
        public KeyIndex KeyIndex { get; private set; }
        public Gtk.Widget GetWidget() => null;

        public Key(NoteFieldSettings noteFieldSettings, KeyIndex index)
        {
            NoteFieldSettings = noteFieldSettings;
            KeyIndex.Value = index.Value;
        }
    }
}
