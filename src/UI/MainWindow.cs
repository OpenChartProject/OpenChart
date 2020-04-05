using Gtk;

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

            var box = new Box(Orientation.Horizontal, 0);
            var noteSkin = App.NoteSkins.GetNoteSkin("default_arrow").GetKeyModeSkin(4);

            box.Add(new Image(noteSkin.Keys[0].TapNote.Pixbuf));
            box.Add(new Image(noteSkin.Keys[1].TapNote.Pixbuf));
            box.Add(new Image(noteSkin.Keys[2].TapNote.Pixbuf));
            box.Add(new Image(noteSkin.Keys[3].TapNote.Pixbuf));

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