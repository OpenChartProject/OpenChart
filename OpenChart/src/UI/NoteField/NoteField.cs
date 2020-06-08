using OpenChart.UI.Widgets;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// The complete representation of a note field. This class includes all of the necessary
    /// components for a note field.
    /// </summary>
    public class NoteField : IWidget
    {
        /// <summary>
        /// An enum of the note field components, in the order that they should be layered.
        /// </summary>
        enum Components
        {
            BeatLines = 0,
            Keys,
        }

        /// <summary>
        /// The beat lines for the note field.
        /// </summary>
        public BeatLines BeatLines { get; private set; }

        /// <summary>
        /// The keys that display the chart objects.
        /// </summary>
        public KeyContainer Keys { get; private set; }

        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        SortedContainer<Components> container;

        /// <summary>
        /// Returns the widget for the note field. This widget is the full size of the note field,
        /// and should be wrapped in a container which can scroll.
        /// </summary>
        public Gtk.Widget GetWidget() => container;

        /// <summary>
        /// Creates a new NoteField instance.
        /// </summary>
        /// <param name="noteFieldSettings">The settings for the note field.</param>
        public NoteField(NoteFieldSettings noteFieldSettings)
        {
            NoteFieldSettings = noteFieldSettings;
            container = new SortedContainer<Components>();
        }

        /// <summary>
        /// Shows the beat lines on the note field.
        /// </summary>
        /// <param name="beatLineSettings">The settings for the beat lines.</param>
        public void ShowBeatLines(BeatLineSettings beatLineSettings)
        {
            BeatLines = new BeatLines(NoteFieldSettings, beatLineSettings);
            container.Add(Components.BeatLines, BeatLines.GetWidget());
        }

        /// <summary>
        /// Shows the keys (chart objects) on the note field.
        /// </summary>
        public void ShowKeys()
        {
            Keys = new KeyContainer(NoteFieldSettings);
            container.Add(Components.Keys, Keys.GetWidget());
        }
    }
}
