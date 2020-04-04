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