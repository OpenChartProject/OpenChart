using Gtk;
using OpenChart.UI.Widgets;

namespace OpenChart.UI
{
    /// <summary>
    /// The main window of the application.
    /// </summary>
    public class MainWindow : Window
    {
        const int InitialWindowWidth = 800;
        const int InitialWindowHeight = 600;
        const int MinimumWindowWidth = 360;
        const int MinimumWindowHeight = 240;

        public MainWindow() : base("OpenChart")
        {
            DeleteEvent += onDelete;

            SetIconFromFile(System.IO.Path.Join("icons", "AppIcon.ico"));

            var keyCount = 4;
            var noteSkin = App.NoteSkins.GetNoteSkin("default_arrow").GetKeyModeSkin(keyCount);
            var noteField = new NoteField(keyCount);

            for (var i = 0; i < 5; i++)
            {
                noteField.Add(
                    new TapNote(
                        noteSkin.Keys[0].TapNote,
                        new OpenChart.Charting.Objects.TapNote(0, i)
                    )
                );

                noteField.Add(
                    new TapNote(
                        noteSkin.Keys[1].TapNote,
                        new OpenChart.Charting.Objects.TapNote(1, i)
                    )
                );

                noteField.Add(
                    new TapNote(
                        noteSkin.Keys[2].TapNote,
                        new OpenChart.Charting.Objects.TapNote(2, i)
                    )
                );

                noteField.Add(
                    new TapNote(
                        noteSkin.Keys[3].TapNote,
                        new OpenChart.Charting.Objects.TapNote(3, i)
                    )
                );
            }

            Add(noteField);

            SetGeometryHints(
                null,
                new Gdk.Geometry
                {
                    MinWidth = MinimumWindowWidth,
                    MinHeight = MinimumWindowHeight,
                },
                Gdk.WindowHints.MinSize
            );

            SetDefaultSize(InitialWindowWidth, InitialWindowHeight);
            ShowAll();
        }

        private void onDelete(object o, DeleteEventArgs e)
        {
            App.Quit();
        }
    }
}
