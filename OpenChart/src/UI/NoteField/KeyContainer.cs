using System.Collections.Generic;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// Note field widget class that contains the key widgets for a chart.
    /// </summary>
    public class KeyContainer : IWidget
    {
        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }
        public Key[] Keys { get; private set; }
        public Gtk.Widget GetWidget() => null;

        public KeyContainer(NoteFieldSettings noteFieldSettings)
        {
            NoteFieldSettings = noteFieldSettings;

            createKeys();
        }

        private void createKeys()
        {
            var keyList = new List<Key>();

            for (var i = 0; i < NoteFieldSettings.Chart.KeyCount.Value; i++)
            {
                keyList.Add(new Key(NoteFieldSettings, i));
            }

            Keys = keyList.ToArray();
        }
    }
}
