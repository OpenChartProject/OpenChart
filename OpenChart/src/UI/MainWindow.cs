using Gtk;
using OpenChart.UI.Widgets;

namespace OpenChart.UI
{
    /// <summary>
    /// The main window of the application.
    /// </summary>
    public class MainWindow : Window
    {
        public MainWindow() : base("OpenChart")
        {
            DeleteEvent += onDelete;

            SetIconFromFile(System.IO.Path.Join("icons", "AppIcon.ico"));

            var keyCount = 4;
            var noteSkin = App.NoteSkins.GetNoteSkin("default_arrow").GetKeyModeSkin(keyCount);
            var noteField = new NoteField(keyCount);

            noteField.Add(
                new TapNote(
                    noteSkin.Keys[0].TapNote,
                    new OpenChart.Charting.Objects.TapNote(0, 0)
                )
            );

            noteField.Add(
                new TapNote(
                    noteSkin.Keys[1].TapNote,
                    new OpenChart.Charting.Objects.TapNote(1, 0)
                )
            );

            noteField.Add(
                new TapNote(
                    noteSkin.Keys[2].TapNote,
                    new OpenChart.Charting.Objects.TapNote(2, 0)
                )
            );

            noteField.Add(
                new TapNote(
                    noteSkin.Keys[3].TapNote,
                    new OpenChart.Charting.Objects.TapNote(3, 0)
                )
            );

            Add(noteField);

            Resize(640, 480);
            ShowAll();
        }

        private void onDelete(object o, DeleteEventArgs e)
        {
            App.Quit();
        }
    }
}
