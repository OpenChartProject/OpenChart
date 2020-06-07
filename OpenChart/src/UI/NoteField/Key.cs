using OpenChart.Charting.Properties;
using OpenChart.UI.Widgets;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// Note field widget class that represents a single key on the note field. The widget is
    /// a simple fixed container that renders the objects for a particular key in the chart.
    /// </summary>
    public class Key : IWidget
    {
        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        /// <summary>
        /// The key index in the chart that this widget represents.
        /// </summary>
        public KeyIndex KeyIndex { get; private set; }

        SortedContainer<double> container;
        public Gtk.Widget GetWidget() => container;

        public Key(NoteFieldSettings noteFieldSettings, KeyIndex index)
        {
            NoteFieldSettings = noteFieldSettings;
            KeyIndex.Value = index.Value;
            container = new SortedContainer<double>();
        }
    }
}
