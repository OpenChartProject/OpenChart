using Gtk;

namespace charter.UI
{
    public class MainWindow : Window
    {
        public MainWindow() : base("Hello")
        {
            DeleteEvent += onDelete;

            Add(new Image("noteskins/default_arrow/tap_1.png"));

            Resize(640, 480);
            ShowAll();
        }

        private void onDelete(object o, DeleteEventArgs e)
        {
            App.Quit();
        }
    }
}