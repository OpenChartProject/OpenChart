using System.Collections.Generic;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// Note field widget class that contains the key widgets for a chart.
    /// </summary>
    public class KeyContainer : IWidget
    {
        public DisplaySettings DisplaySettings { get; private set; }
        public Key[] Keys { get; private set; }
        public Gtk.Widget GetWidget() => null;

        public KeyContainer(DisplaySettings settings)
        {
            DisplaySettings = settings;

            createKeys();
        }

        private void createKeys()
        {
            var keyList = new List<Key>();

            for (var i = 0; i < DisplaySettings.Chart.KeyCount.Value; i++)
            {
                keyList.Add(new Key(DisplaySettings, i));
            }

            Keys = keyList.ToArray();
        }
    }
}
