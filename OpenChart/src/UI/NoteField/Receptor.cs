using OpenChart.Charting.Properties;
using OpenChart.UI.Widgets;

namespace OpenChart.UI.NoteField
{
    public class Receptor : IWidget
    {
        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        /// <summary>
        /// The key index in the chart that this widget represents.
        /// </summary>
        public KeyIndex KeyIndex { get; private set; }

        Image image;

        public Gtk.Widget GetWidget() => image;

        public Receptor(NoteFieldSettings noteFieldSettings, KeyIndex index)
        {
            NoteFieldSettings = noteFieldSettings;
            KeyIndex = index;
            image = new Image(NoteFieldSettings.NoteSkin.Keys[KeyIndex.Value].Receptor);
        }
    }
}
