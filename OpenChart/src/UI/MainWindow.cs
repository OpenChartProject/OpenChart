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

            var box = new Box(Orientation.Horizontal, 0);
            var noteSkin = App.NoteSkins.GetNoteSkin("default_arrow").GetKeyModeSkin(4);

            box.Add(new NoteWidget(0, noteSkin.Keys[0].TapNote));
            box.Add(new NoteWidget(0, noteSkin.Keys[1].TapNote));
            box.Add(new NoteWidget(0, noteSkin.Keys[2].TapNote));
            box.Add(new NoteWidget(0, noteSkin.Keys[3].TapNote));

            Add(box);

            Resize(640, 480);
            ShowAll();
        }

        private void onDelete(object o, DeleteEventArgs e)
        {
            App.Quit();
        }
    }
}