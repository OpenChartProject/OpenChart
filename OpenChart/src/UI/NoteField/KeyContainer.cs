using OpenChart.Charting;
using System.Collections.Generic;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// Note field widget class that contains the key widgets for a chart. It also watches the chart
    /// for changes and updates the keys as needed.
    /// </summary>
    public class KeyContainer : IWidget
    {
        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        /// <summary>
        /// The list of Key instances in this container.
        /// </summary>
        public List<Key> Keys { get; private set; }

        Gtk.HBox container;

        /// <summary>
        /// Returns the widget for all of the keys in a container.
        /// </summary>
        /// <returns></returns>
        public Gtk.Widget GetWidget() => container;

        /// <summary>
        /// Creates a new KeyContainer instance.
        /// </summary>
        public KeyContainer(NoteFieldSettings noteFieldSettings)
        {
            Keys = new List<Key>();
            NoteFieldSettings = noteFieldSettings;
            container = new Gtk.HBox();

            // Create a Key instance for the number of keys in the chart.
            for (var i = 0; i < NoteFieldSettings.Chart.KeyCount.Value; i++)
            {
                var key = new Key(NoteFieldSettings, i);
                Keys.Add(key);
                container.Add(key.GetWidget());

                foreach (var obj in NoteFieldSettings.Chart.Objects[i])
                {
                    key.AddObject(obj);
                }
            }

            NoteFieldSettings.ChartEventBus.ObjectAdded += onObjectAdded;
            NoteFieldSettings.ChartEventBus.ObjectRemoved += onObjectRemoved;
        }

        private void onObjectAdded(object o, ObjectEventArgs e)
        {
            Keys[e.Object.KeyIndex.Value].AddObject(e.Object);
        }

        private void onObjectRemoved(object o, ObjectEventArgs e)
        {
            Keys[e.Object.KeyIndex.Value].RemoveObject(e.Object);
        }
    }
}
