using System.Collections.Generic;

namespace OpenChart.UI.NoteField
{
    public class ReceptorContainer : IWidget
    {
        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        /// <summary>
        /// The list of Receptor instances in this container.
        /// </summary>
        public List<Receptor> Receptors { get; private set; }

        Gtk.Fixed positionContainer;
        Gtk.HBox hboxContainer;

        public Gtk.Widget GetWidget() => positionContainer;

        public ReceptorContainer(NoteFieldSettings noteFieldSettings)
        {
            Receptors = new List<Receptor>();
            NoteFieldSettings = noteFieldSettings;
            hboxContainer = new Gtk.HBox();
            positionContainer = new Gtk.Fixed();

            // Create a Receptor instance for the number of keys in the chart.
            for (var i = 0; i < NoteFieldSettings.Chart.KeyCount.Value; i++)
            {
                var receptor = new Receptor(NoteFieldSettings, i);
                Receptors.Add(receptor);
                hboxContainer.Add(receptor.GetWidget());
            }

            positionContainer.Add(hboxContainer);

            NoteFieldSettings.ReceptorPositionChanged += delegate
            {
                var y = NoteFieldSettings.TimeToPosition(NoteFieldSettings.ReceptorTime);
                positionContainer.Move(hboxContainer, 0, y);
            };
        }
    }
}
