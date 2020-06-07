using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using OpenChart.UI.NoteField.Objects;
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

        SortedContainer<Beat> container;
        public Gtk.Widget GetWidget() => container;

        /// <summary>
        /// Creates a new Key instance.
        /// </summary>
        public Key(NoteFieldSettings noteFieldSettings, KeyIndex index)
        {
            NoteFieldSettings = noteFieldSettings;
            KeyIndex = index;
            container = new SortedContainer<Beat>();
        }

        /// <summary>
        /// Adds a note field object to the key.
        /// </summary>
        public void AddObject(BaseObject chartObject)
        {
            var noteFieldObject = NoteFieldSettings.ObjectFactory.Create(chartObject);

            container.Put(
                noteFieldObject.GetChartObject().Beat,
                noteFieldObject.GetWidget(),
                0,
                NoteFieldSettings.BeatToPosition(noteFieldObject.GetChartObject().Beat)
            );
        }

        /// <summary>
        /// Removes a note field object.
        /// </summary>
        public void RemoveObject(BaseObject obj)
        {
            container.Remove(obj.Beat);
        }
    }
}
