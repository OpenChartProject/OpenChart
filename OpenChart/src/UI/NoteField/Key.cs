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

        /// <summary>
        /// Returns the widget for the key.
        /// </summary>
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

            noteFieldObject.GetWidget().SizeAllocated += delegate
            {
                UpdateObjectPosition(noteFieldObject);
            };

            container.Add(noteFieldObject.GetChartObject().Beat, noteFieldObject.GetWidget());
        }

        /// <summary>
        /// Gets the pixel offset of the object given the current alignment settings.
        /// </summary>
        public int GetObjectOffset(INoteFieldObject obj)
        {
            if (NoteFieldSettings.Alignment == NoteFieldObjectAlignment.Top)
                return 0;
            else if (NoteFieldSettings.Alignment == NoteFieldObjectAlignment.Center)
                return obj.GetHeight() / 2;
            else if (NoteFieldSettings.Alignment == NoteFieldObjectAlignment.Bottom)
                return obj.GetHeight();

            return 0;
        }

        /// <summary>
        /// Removes a note field object.
        /// </summary>
        public void RemoveObject(BaseObject obj)
        {
            container.Remove(obj.Beat);
        }

        /// <summary>
        /// Updates the object's position.
        /// </summary>
        public void UpdateObjectPosition(INoteFieldObject noteFieldObject)
        {
            var y = NoteFieldSettings.BeatToPosition(noteFieldObject.GetChartObject().Beat);
            y -= GetObjectOffset(noteFieldObject);

            container.Move(noteFieldObject.GetWidget(), 0, y);
        }
    }
}
